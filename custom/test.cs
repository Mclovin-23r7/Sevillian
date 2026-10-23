/*
Script for running unit tests
Please run from the Sevillian root directory
*/
using System.Diagnostics;

var testDir = $"{Directory.GetCurrentDirectory()}/test";
Console.WriteLine(testDir);
var psi = new ProcessStartInfo("dotnet") { testDir };
psi.ArgumentList.Add("test");
psi.ArgumentList.Add("--collect:Xplat Code Coverage");
psi.ArgumentList.Add("--settings");
psi.ArgumentList.Add("coverlet.runsettings");
foreach (var a in args) psi.ArgumentList.Add(a);

using var p = Process.Start(psi);
p.WaitforExit();
return p.ExitCode;