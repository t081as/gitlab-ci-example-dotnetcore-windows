using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.ReportGenerator;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class BuildTargets : NukeBuild
{
    public static int Main () => Execute<BuildTargets>(x => x.All);

    [Parameter("Configuration to build")]
    readonly Configuration Configuration = Configuration.Debug;

    [Solution] readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath BuildDirectory => OutputDirectory / "build";
    AbsolutePath PublishDirectory => OutputDirectory / "publish";
    AbsolutePath CoverageDirectory => OutputDirectory / "coverage";
    AbsolutePath MainProjectFile => SourceDirectory / "MyProject" / "MyProject.csproj";

    readonly string GlobCoverageFiles = "**/TestResults/*/coverage.cobertura.xml";
    readonly string GlobTestResultFiles = "**/TestResults/TestResults.xml";

    Target Clean => _ => _
        .Executes(() =>
        {
            RootDirectory.GlobFiles(GlobCoverageFiles).ForEach(DeleteFile);
            RootDirectory.GlobFiles(GlobTestResultFiles).ForEach(DeleteFile);
            RootDirectory.GlobFiles("*.zip").ForEach(DeleteFile);

            EnsureCleanDirectory(BuildDirectory);
            EnsureCleanDirectory(PublishDirectory);
            EnsureCleanDirectory(CoverageDirectory);
            EnsureCleanDirectory(OutputDirectory);
            DotNetClean();
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            if (Configuration == Configuration.Release)
            {
                DotNetBuild(_ => _
                    .SetProjectFile(MainProjectFile)
                    .SetOutputDirectory(BuildDirectory)
                    .SetConfiguration(Configuration)
                    .AddProperty("DebugType", "None")
                    .AddProperty("DebugSymbols", "false")
                    .EnableNoRestore());
            }
            else
            {
                DotNetBuild(_ => _
                    .SetProjectFile(Solution)
                    .SetOutputDirectory(BuildDirectory)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore());
            }
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .OnlyWhenStatic(() => Configuration == Configuration.Debug)
        .Executes(() =>
        {
            string loggerConfiguration = $"junit;LogFilePath={OutputDirectory / "TestResults" / "TestResults.xml"};MethodFormat=Class;FailureBodyFormat=Verbose";

            DotNetTest(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetLogger(loggerConfiguration) // MyProject.Tests.csproj, package JunitXml.TestLogger
                .SetDataCollector("XPlat Code Coverage") // MyProject.Tests.csproj, package coverlet.collector
                .EnableNoRestore());

            if (IsWin)
            {
                ReportGenerator(_ => _ // Build.csproj, package ReportGenerator
                    .SetToolPath(ToolPathResolver.GetPackageExecutable("ReportGenerator", "ReportGenerator.exe", null, "netcoreapp3.0"))
                    .SetReports(RootDirectory / GlobCoverageFiles)
                    .SetTargetDirectory(CoverageDirectory)
                    .SetReportTypes(ReportTypes.TextSummary, ReportTypes.Html));

                // Coverage information will be emitted to build log for
                // coverage regex in .gitlab-ci.yml file
                Logger.Info(File.ReadAllText(CoverageDirectory / "Summary.txt"));
            }
        });

    Target Publish => _ => _
        .DependsOn(Compile)
        .OnlyWhenStatic(() => Configuration == Configuration.Release)
        .Executes(() =>
        {
            DotNetPublish(_ => _
                .SetConfiguration(Configuration)
                .AddProperty("DebugType", "None")
                .AddProperty("DebugSymbols", "false")
                .SetProject(MainProjectFile)
                .SetOutput(PublishDirectory / "win-x64")
                .EnableSelfContained()
                .AddProperty("PublishSingleFile", "true")
                .AddProperty("PublishTrimmed", "true")
                .SetRuntime("win-x64"));

            DotNetPublish(_ => _
                .SetConfiguration(Configuration)
                .AddProperty("DebugType", "None")
                .AddProperty("DebugSymbols", "false")
                .SetProject(MainProjectFile)
                .SetOutput(PublishDirectory / "win-x86")
                .EnableSelfContained()
                .AddProperty("PublishSingleFile", "true")
                .AddProperty("PublishTrimmed", "true")
                .SetRuntime("win-x86"));

            CompressionTasks.CompressZip(
                BuildDirectory,
                RootDirectory / "MyProject-windows-any.zip",
                null,
                System.IO.Compression.CompressionLevel.Optimal,
                System.IO.FileMode.CreateNew);

            CompressionTasks.CompressZip(
                PublishDirectory / "win-x64",
                RootDirectory / "MyProject-windows-amd64.zip",
                null,
                System.IO.Compression.CompressionLevel.Optimal,
                System.IO.FileMode.CreateNew);

            CompressionTasks.CompressZip(
                PublishDirectory / "win-x86",
                RootDirectory / "MyProject-windows-i386.zip",
                null,
                System.IO.Compression.CompressionLevel.Optimal,
                System.IO.FileMode.CreateNew);
        });

    Target All => _ => _
        .DependsOn(Publish, Test);

}
