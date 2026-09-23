using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply read‑only protection flags (assign BOOL enum directly, no .Value)
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;   // protect backgrounds
            diagram.DocumentSettings.ProtectMasters = BOOL.True; // protect masters
            diagram.DocumentSettings.ProtectShapes = BOOL.True;  // protect shapes
            diagram.DocumentSettings.ProtectStyles = BOOL.True;  // protect styles

            // Output file path for the protected diagram
            string outputPath = "output_protected.vsdx";

            // Save the diagram with protection applied
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}