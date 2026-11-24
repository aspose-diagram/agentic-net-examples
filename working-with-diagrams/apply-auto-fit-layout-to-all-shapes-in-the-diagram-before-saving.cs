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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // The Aspose.Diagram API does not expose a direct AutoFit method on Shape.
                // Instead, enable auto‑fit of the page to its drawing content when saving.
                // This ensures all shapes are fitted within the page bounds.

                // Create save options and enable auto‑fit.
                DiagramSaveOptions saveOptions = new DiagramSaveOptions();
                saveOptions.AutoFitPageToDrawingContent = true;

                // Save the modified diagram using the save options.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}