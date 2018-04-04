cd /D "%~dp0"
cd ..\..\..\LocalNuGet
nuget push ShouldAssertions.1.0.1.nupkg -Source https://api.nuget.org/v3/index.json