using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Required minimum version and build number
    private static readonly Version RequiredVersion = new Version("23.9");
    private const int RequiredBuildNumber = 12345; // example required build

    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        Diagram diagram;
        try
        {
            // Load the diagram from the input file
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Verify library version
        Version currentVersion;
        try
        {
            // diagram.Version returns a string; parse it into a Version object
            currentVersion = new Version(diagram.Version);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unable to parse Aspose.Diagram version: {ex.Message}");
            return;
        }

        if (currentVersion < RequiredVersion)
        {
            Console.Error.WriteLine($"Aspose.Diagram version {currentVersion} is older than required {RequiredVersion}. Aborting conversion.");
            return;
        }

        // Verify build number (DocumentProps.BuildNumberCreated is a string; parse to int)
        int currentBuild;
        try
        {
            currentBuild = int.Parse(diagram.DocumentProps.BuildNumberCreated);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse build number: {ex.Message}");
            return;
        }

        if (currentBuild < RequiredBuildNumber)
        {
            Console.Error.WriteLine($"Aspose.Diagram build number {currentBuild} is older than required {RequiredBuildNumber}. Aborting conversion.");
            return;
        }

        try
        {
            // If checks pass, proceed with conversion (example: save as VSDX)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}