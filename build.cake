//////////////////////////////////////////////////////////////////////
// ARGUMENTOS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// CAMINHOS
//////////////////////////////////////////////////////////////////////

var solution = "./Convoquei.sln";
var testProject = "./Convoquei.Core.Tests/Convoquei.Core.Tests.csproj";
var coverageOutput = "./coverage";
var coberturaFile = $"{coverageOutput}/coverage.cobertura.xml";
var reportOutput = "./coveragereport";

//////////////////////////////////////////////////////////////////////
// FERRAMENTAS
//////////////////////////////////////////////////////////////////////

#tool nuget:?package=reportgenerator
#addin nuget:?package=Cake.DotNetTool.Module

//////////////////////////////////////////////////////////////////////
// TAREFAS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(coverageOutput);
    CleanDirectory(reportOutput);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetRestore(solution);
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    DotNetBuild(solution, new DotNetBuildSettings {
        Configuration = configuration
    });
});

Task("Test-With-Coverage")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetTest(testProject, new DotNetTestSettings {
        Configuration = configuration,
        NoBuild = true,
        ArgumentCustomization = args => args
            .Append("--collect:\"XPlat Code Coverage\"")
            .Append("--results-directory")
            .Append(coverageOutput)
            .Append("-- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=cobertura")
    });
});

Task("Generate-Report")
    .IsDependentOn("Test-With-Coverage")
    .Does(() =>
{
    var coberturaFiles = GetFiles($"{coverageOutput}/**/coverage.cobertura.xml");
    
    if (coberturaFiles.Count == 0)
    {
        throw new Exception("Arquivo coverage.cobertura.xml não encontrado.");
    }

    var coberturaFile = coberturaFiles.First().FullPath;

    StartProcess("reportgenerator", new ProcessSettings {
        Arguments = $"-reports:{coberturaFile} -targetdir:{reportOutput} -reporttypes:Html"
    });
});

Task("Abrir-Relatorio")
    .IsDependentOn("Generate-Report")
    .Does(() =>
{
    var indexFile = $"{reportOutput}/index.html";
    if (!System.IO.File.Exists(indexFile))
    {
        throw new Exception("Relatório HTML não encontrado.");
    }

    StartProcess("cmd", new ProcessSettings {
        Arguments = $"/c start {indexFile}"
    });
});

Task("Default")
    .IsDependentOn("Abrir-Relatorio");

//////////////////////////////////////////////////////////////////////
// EXECUÇÃO
//////////////////////////////////////////////////////////////////////

RunTarget(target);
