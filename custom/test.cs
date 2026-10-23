/*
Script for running unit tests, generating coverage report and serving report over the local network
Please run from the Sevillian root directory
*/
using System.Diagnostics;

try{
    var testDir = $"{Directory.GetCurrentDirectory()}/test";
    var resultDir = $"{testDir}/TestResults";
    var coverageDir = $"{testDir}/coveragereport";

    ProcessStartInfo pDotnet = new()
    {
        UseShellExecute = false,
        FileName = "dotnet",
        WorkingDirectory = testDir
    };
    pDotnet.ArgumentList.Add("test");
    pDotnet.ArgumentList.Add("--collect:Xplat Code Coverage");
    pDotnet.ArgumentList.Add("--settings");
    pDotnet.ArgumentList.Add("coverlet.runsettings");

    Console.WriteLine($"Starting unit tests in {testDir}");
    Process.Start(pDotnet).WaitForExit();

    ProcessStartInfo pReportGen = new()
    {
        UseShellExecute = false,
        FileName = "dotnet",
        WorkingDirectory = resultDir
    };
    pReportGen.ArgumentList.Add("tool");
    pReportGen.ArgumentList.Add("run");
    pReportGen.ArgumentList.Add("reportgenerator");
    pReportGen.ArgumentList.Add($"-reports:{resultDir}/*/coverage.cobertura.xml");
    pReportGen.ArgumentList.Add("-targetdir:\"../coveragereport\"");
    pReportGen.ArgumentList.Add("-reporttypes:Html");

    Console.WriteLine($"Generating coverage report");
    Process.Start(pReportGen).WaitForExit();

    ProcessStartInfo pStartServer = new()
    {
        UseShellExecute = false,
        FileName = "python3",
        WorkingDirectory = coverageDir
    };
    pStartServer.ArgumentList.Add("-m");
    pStartServer.ArgumentList.Add("http.server");
    pStartServer.ArgumentList.Add("8000");
    Process.Start(pStartServer).WaitForExit();
}
catch (Exception e)
{
    Console.WriteLine(e);
}