using CommandLine;
using CommandLine.Text;
using Engine.Data;

namespace Engine;

/// <summary>
/// This is where we parse the different CLI arguments like enabling the writing of Logs.
/// </summary>

public static class EngineArgParser
{

    public static void ArgInput(string[] args)
    {
        Parser.Default.ParseArguments<CliParserOptions>(args)
        .WithParsed<CliParserOptions>(o =>
        {
            if(o.SkipLogos)
            {
                Settings.cVars.SplashScreenLength = 0.1f;
            }
            if(o.Logging)
            {
                Settings.cVars.WriteTraceLog = true;
            }   
        });
    }
}

public class CliParserOptions
{
    [Option('s', "Skip Logo Splash", Required = false, HelpText = "Set Logo Splash screen to 0.1f.")]
    public bool SkipLogos { get; set; }
    [Option('l', "Logging", Required = false, HelpText = "Outputs a Logfile in the root of the gamefolder.")]
    public bool Logging { get; set; }
}