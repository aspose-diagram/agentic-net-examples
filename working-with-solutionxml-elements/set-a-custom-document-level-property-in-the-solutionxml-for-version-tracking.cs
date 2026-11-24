using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not supplied, create a new file name with a suffix.
        string outputPath;
        if (args.Length > 1)
        {
            outputPath = args[1];
        }
        else
        {
            string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_updated.vsdx");
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create a SolutionXML element to store version tracking information.
            SolutionXML versionInfo = new SolutionXML();
            versionInfo.Name = "VersionInfo";                     // Identifier for the custom XML.
            versionInfo.XmlValue = $"<Version>{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC</Version>";

            // Add the SolutionXML element to the document‑level collection.
            diagram.SolutionXMLs.Add(versionInfo);

            // Save the modified diagram using the VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}