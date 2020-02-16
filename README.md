# GITLAB-CI EXAMPLE: DOTNET CORE WINDOWS

[![pipeline status](https://gitlab.com/tobiaskoch/gitlab-ci-example-dotnetcore-windows/badges/master/pipeline.svg)](https://gitlab.com/tobiaskoch/gitlab-ci-example-dotnetcore/commits/master)
[![maintained: yes](https://tobiaskoch.gitlab.io/badges/maintained-yes.svg)](https://gitlab.com/tobiaskoch/gitlab-ci-example-dotnetcore-windows/commits/master)
[![donate: paypal](https://tobiaskoch.gitlab.io/badges/donate-paypal.svg)](https://www.tk-software.de/donate)

---
This is a [gitlab continuous integration](https://about.gitlab.com/features/gitlab-ci-cd/) example project compatible with the [windows-based shared runners](https://about.gitlab.com/blog/2020/01/21/windows-shared-runner-beta/) provided on
[gitlab.com](https://gitlab.com) building a .NET Core Windows Desktop project.

This example repository uses [Nuke Build](https://nuke.build/) to build the project.

## Debug
The following commands will trigger a debug build:

    > .\build.ps1

or

    > .\build.ps1 --configuration Debug

- The [nuke build script](https://gitlab.com/tobiaskoch/gitlab-ci-example-dotnetcore-windows/-/blob/master/build/BuildTargets.cs) uses [JUnitXml.TestLogger](https://www.nuget.org/packages/JunitXml.TestLogger/) to generate an [unit test result file that can be parsed by gitlab](https://docs.gitlab.com/ee/ci/junit_test_reports.html)
- The [nuke build script](https://gitlab.com/tobiaskoch/gitlab-ci-example-dotnetcore-windows/-/blob/master/build/BuildTargets.cs) uses [Coverlet.Collector](https://www.nuget.org/packages/coverlet.collector/) together with [ReportGenerator](https://www.nuget.org/packages/ReportGenerator/) to generate a html code coverage report and [allow gitlab to display the code coverage](https://docs.gitlab.com/ee/user/project/pipelines/settings.html#test-coverage-parsing) of the unit tests

## Release
The following command will trigger a release build:

    > .\build.ps1 --configuration Release

- The [nuke build script](https://gitlab.com/tobiaskoch/gitlab-ci-example-dotnetcore-windows/-/blob/master/build/BuildTargets.cs) will create the following zip archives in the root directory of the repository:
  - MyProject-windows-any.zip (will require .NET Core Runtime 3.1)
  - MyProject-windows-i386.zip (will **not** require .NET Core Runtime 3.1)
  - MyProject-windows-amd64.zip (will **not** require .NET Core Runtime 3.1)

## Image
Thanks to the [Tango Desktop Project](http://tango.freedesktop.org) for the repository icon.

## Contributing
see [CONTRIBUTING.md](https://gitlab.com/tobiaskoch/gitlab-ci-example-dotnetcore-windows/blob/master/CONTRIBUTING.md)

## Donating
Thanks for your interest in this project. You can show your appreciation and support further development by [donating](https://www.tk-software.de/donate).

## License
**gitlab-ci-example-dotnetcore** © 2019-2020  [Tobias Koch](https://www.tk-software.de). Released under the [MIT License](https://gitlab.com/tobiaskoch/gitlab-ci-example-dotnetcore-windows/blob/master/LICENSE.md).