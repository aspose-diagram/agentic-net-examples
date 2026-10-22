using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_protected.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Set global document protection to read‑only mode.
            // Aspose.Diagram provides protection flags on DocumentSettings.
            // Setting all protection flags to TRUE makes the document read‑only.
            // ------------------------------------------------------------
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
            diagram.DocumentSettings.ProtectMasters = BOOL.True;
            diagram.DocumentSettings.ProtectShapes = BOOL.True;
            diagram.DocumentSettings.ProtectStyles = BOOL.True;

            // ------------------------------------------------------------
            // NOTE: Aspose.Diagram does not expose a property to assign a
            // password for editing restrictions on a Visio file. Therefore,
            // a password cannot be set via the API. The document is now
            // protected against modifications as per the available API.
            // ------------------------------------------------------------

            // Save the protected diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with protection to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}