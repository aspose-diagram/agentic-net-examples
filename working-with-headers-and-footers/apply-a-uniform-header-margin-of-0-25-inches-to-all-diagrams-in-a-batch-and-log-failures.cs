using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing diagrams; default to "Diagrams" if not provided
        string inputFolder = args.Length > 0 ? args[0] : "Diagrams";

        // Output folder for processed diagrams; default to "Processed"
        string outputFolder = args.Length > 1 ? args[1] : "Processed";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Get all Visio files in the input folder (common extensions)
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            // Filter supported Visio extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
            {
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Apply a uniform header margin of 0.25 inches
                diagram.HeaderFooter.HeaderMargin.Value = 0.25;

                // Determine output path (same file name in the output folder)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                // Save the diagram using VSDX format (works for all supported types)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Successfully processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Log any failures without stopping the batch
                Console.WriteLine($"Failed to process '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }
}
