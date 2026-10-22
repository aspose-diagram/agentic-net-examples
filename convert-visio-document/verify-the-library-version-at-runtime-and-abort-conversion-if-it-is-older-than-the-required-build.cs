using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <requiredLibraryVersion>");
            Console.Error.WriteLine("Example: DiagramConverter.exe diagram.vsdx output.vsdx 23.10.0.0");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string requiredVersionString = args[2];

        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse the required version string.
        if (!Version.TryParse(requiredVersionString, out Version requiredVersion))
        {
            Console.Error.WriteLine($"Invalid required version format: {requiredVersionString}");
            return;
        }

        // Retrieve the Aspose.Diagram assembly version at runtime.
        Version libraryVersion = typeof(Diagram).Assembly.GetName().Version;

        // Compare library version with the required minimum version.
        if (libraryVersion == null || libraryVersion < requiredVersion)
        {
            Console.Error.WriteLine($"Aspose.Diagram library version {libraryVersion?.ToString() ?? "unknown"} is older than the required version {requiredVersion}.");
            Console.Error.WriteLine("Aborting conversion.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Perform conversion – here we simply save to the requested output format (VSDX).
            // Ensure the Save method includes a valid SaveFileFormat argument.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Conversion succeeded. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine("An error occurred during conversion:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}