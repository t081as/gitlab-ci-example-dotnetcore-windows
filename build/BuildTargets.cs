using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

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
    AbsolutePath CoverageDirectory => OutputDirectory / "coverage";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(_ => _
                    .SetProjectFile(Solution)
                    .SetOutputDirectory(BuildDirectory)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .OnlyWhenStatic(() => Configuration == Configuration.Debug)
        .Executes(() =>
        {
            if (Configuration == Configuration.Debug)
            {
                DotNetTest(_ => _
                    .SetProjectFile(Solution)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore()
                    .EnableNoBuild());
            }
        });

    Target Publish => _ => _
        .DependsOn(Compile)
        .OnlyWhenStatic(() => Configuration == Configuration.Release)
        .Executes(() =>
        {
            if (Configuration == Configuration.Debug)
            {
                DotNetTest(_ => _
                    .SetProjectFile(Solution)
                    .SetConfiguration(Configuration)
                    .EnableNoRestore()
                    .EnableNoBuild());
            }
        });

    Target All => _ => _
        .DependsOn(Publish, Test);

}
