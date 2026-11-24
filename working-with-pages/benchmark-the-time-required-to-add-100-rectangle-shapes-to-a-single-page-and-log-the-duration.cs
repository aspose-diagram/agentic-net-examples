using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page (index 0) for adding shapes
                int pageIndex = 0;

                // Start timing
                Stopwatch sw = Stopwatch.StartNew();

                // Add 100 rectangle shapes
                for (int i = 0; i < 100; i++)
                {
                    // Simple layout: arrange shapes in a grid
                    double pinX = (i % 10) * 2.0 + 1.0; // X coordinate
                    double pinY = (i / 10) * 2.0 + 1.0; // Y coordinate

                    // Add shape; returns the shape ID as a long
                    long shapeId = diagram.AddShape(pinX, pinY, "Rectangle", pageIndex);

                    // Retrieve the shape if further customization is needed (not required here)
                    // Shape shape = diagram.Pages[pageIndex].Shapes.GetShape(shapeId);
                }

                // Stop timing
                sw.Stop();

                // Log the duration in milliseconds
                Console.WriteLine($"Added 100 rectangle shapes in {sw.ElapsedMilliseconds} ms.");

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during Aspose operations
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}