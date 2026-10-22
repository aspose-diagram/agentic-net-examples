using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page for the benchmark
                Page page = diagram.Pages[0];

                // Parameters for shape creation
                const int shapeCount = 5000;          // Number of shapes to add (thousands)
                const double startX = 1.0;            // Starting X position (in inches)
                const double startY = 1.0;            // Starting Y position (in inches)
                const double offsetX = 0.5;           // Horizontal offset between shapes
                const double offsetY = 0.5;           // Vertical offset between shapes
                const string masterName = "Rectangle"; // Master name for the shape

                // Add a large number of shapes to the page
                for (int i = 0; i < shapeCount; i++)
                {
                    double x = startX + (i % 100) * offsetX;
                    double y = startY + (i / 100) * offsetY;

                    // AddShape returns a long identifier
                    long shapeId = page.AddShape(x, y, masterName, false);

                    // Retrieve the shape object to modify its text
                    Shape shape = page.Shapes.GetShape(shapeId);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Shape {i + 1}"));
                }

                // Prepare AutoSpaceOptions (horizontal and vertical distances)
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                // Set spacing values (in inches)
                autoSpaceOptions.DistanceInHorizontal = 0.2;
                autoSpaceOptions.DistanceInVertical = 0.2;

                // Benchmark the AutoSpaceShapes operation
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                // Perform auto‑spacing on the entire shape collection of the page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                stopwatch.Stop();

                // Output the elapsed time
                Console.WriteLine($"Auto‑spacing of {shapeCount} shapes completed in {stopwatch.Elapsed.TotalSeconds:F3} seconds.");

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}