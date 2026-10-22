using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Compress VBA project:
                // Removing the VBA project data reduces the file size.
                // This is achieved by setting the VbProjectData property to null.
                // ------------------------------------------------------------
                diagram.VbProjectData = null;

                // Save the modified diagram. Use VSDX format (non‑macro) as VBA data is removed.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}