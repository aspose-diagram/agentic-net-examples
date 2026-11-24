using System;
using System.IO;
using Aspose.Diagram;

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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Count total number of shapes across all pages.
            int totalShapeCount = 0;
            foreach (Page page in diagram.Pages)
            {
                totalShapeCount += page.Shapes.Count;
            }

            // Determine new page height. Example: 0.5 inches per shape.
            double newPageHeight = totalShapeCount * 0.5;

            // Apply the new height to each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Page height is stored in inches.
                page.PageSheet.PageProps.PageHeight.Value = newPageHeight;
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}