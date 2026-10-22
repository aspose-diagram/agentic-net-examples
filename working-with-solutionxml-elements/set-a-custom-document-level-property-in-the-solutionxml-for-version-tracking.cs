using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [version]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string version = args.Length > 2 ? args[2] : "1.0.0";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create a new SolutionXML element to store version information.
            SolutionXML versionXml = new SolutionXML();
            versionXml.Name = "VersionInfo";
            versionXml.XmlValue = $"<Version>{version}</Version>";

            // Add the SolutionXML element to the document.
            diagram.SolutionXMLs.Add(versionXml);

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Successfully added version info and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}