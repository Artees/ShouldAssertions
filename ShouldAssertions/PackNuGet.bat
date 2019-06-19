cd /D "%~dp0"
msbuild ShouldAssertions.csproj /t:rebuild /verbosity:quiet /p:Configuration=Release
nuget pack ShouldAssertions.csproj -OutputDirectory ..\..\..\LocalNuGet -Prop Configuration=Release