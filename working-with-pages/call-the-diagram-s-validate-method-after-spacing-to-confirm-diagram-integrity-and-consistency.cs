using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists before loading
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Auto‑space shapes on each page
        try
        {
            foreach (Page page in diagram.Pages)
            {
                // Configure spacing options (in inches)
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 0.5,
                    DistanceInVertical = 0.5
                };

                // Apply auto‑spacing to all shapes on the current page
                page.AutoSpaceShapes(page.Shapes, options);
            }

            // Validate the diagram by attempting a save to a memory stream.
            // If the save succeeds without exception, the diagram is considered valid.
            using (MemoryStream validationStream = new MemoryStream())
            {
                diagram.Save(validationStream, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram validation completed successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during spacing or validation
            Console.Error.WriteLine($"Diagram processing failed: {ex.Message}");
            return;
        }

        // Guard: ensure the output directory exists before saving
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Save the updated diagram to the specified output path
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}