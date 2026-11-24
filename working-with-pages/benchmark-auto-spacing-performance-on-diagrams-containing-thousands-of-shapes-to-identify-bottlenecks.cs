using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard for input file existence.
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
                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page for adding shapes.
                Page page = diagram.Pages[0];

                // Define the number of shapes to add for the benchmark.
                const int shapeCount = 5000;
                const string masterName = "Rectangle"; // Assumes the master exists in the stencil.

                // Add shapes in a grid layout to avoid initial overlapping.
                int columns = (int)Math.Ceiling(Math.Sqrt(shapeCount));
                double startX = 1.0;
                double startY = 1.0;
                double offsetX = 2.0;
                double offsetY = 2.0;

                for (int i = 0; i < shapeCount; i++)
                {
                    int col = i % columns;
                    int row = i / columns;
                    double x = startX + col * offsetX;
                    double y = startY + row * offsetY;

                    // AddShape returns a long shape ID.
                    long shapeId = page.AddShape(x, y, masterName, false);

                    // Retrieve the shape object for further modifications.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Clear any existing text and add new text.
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"Shape {i + 1}"));
                }

                // Prepare AutoSpaceOptions with desired spacing.
                AutoSpaceOptions options = new AutoSpaceOptions();
                options.DistanceInHorizontal = 0.5; // inches
                options.DistanceInVertical = 0.5;   // inches

                // Benchmark the AutoSpaceShapes operation.
                Stopwatch sw = Stopwatch.StartNew();

                // Auto-space all shapes on the page.
                page.AutoSpaceShapes(page.Shapes, options);

                sw.Stop();

                Console.WriteLine($"Auto-space of {shapeCount} shapes completed in {sw.ElapsedMilliseconds} ms.");

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}