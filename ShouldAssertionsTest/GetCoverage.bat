set dll=..\ShouldAssertionsUnity\Assets\Tests\Play\Debug\ShouldAssertionsTest.dll
..\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:..\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe -targetargs:%dll% -register:user -filter:+[*]* -output:TestReport.xml
..\packages\ReportGenerator.3.1.2\tools\ReportGenerator.exe -reports:TestReport.xml -targetdir:coverage -reporttypes:"Html;Badges"
coverage\index.htm