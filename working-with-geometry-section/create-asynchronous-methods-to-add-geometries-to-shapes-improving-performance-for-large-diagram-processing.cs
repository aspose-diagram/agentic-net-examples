using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Asynchronously adds a simple geometry (a line from (0,0) to the shape's width/height) to a shape.
    private static async Task AddGeometryAsync(Diagram diagram, long shapeId)
    {
        await Task.Run(() =>
        {
            // Locate the shape by its ID across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    targetShape = page.Shapes.GetShape(shapeId);
                    if (targetShape != null)
                    {
                        break;
                    }
                }
                catch
                {
                    // GetShape throws if the ID is not on this page; ignore and continue.
                }
            }

            // If the shape was not found, exit the task.
            if (targetShape == null)
            {
                return;
            }

            // Ensure the shape has at least one geometry container.
            if (targetShape.Geoms.Count == 0)
            {
                targetShape.Geoms.Add(new Geom());
            }

            // Use the first geometry container.
            Geom geom = targetShape.Geoms[0];

            // Create a MoveTo segment at the origin (0,0).
            MoveTo move = new MoveTo();
            move.X.Value = 0.0;
            move.Y.Value = 0.0;
            geom.CoordinateCol.Add(move);

            // Create a LineTo segment that reaches the shape's width and height.
            LineTo line = new LineTo();
            line.X.Value = targetShape.XForm.Width.Value;
            line.Y.Value = targetShape.XForm.Height.Value;
            geom.CoordinateCol.Add(line);
        });
    }

    // Processes the entire diagram asynchronously, adding geometry to each shape.
    private static async Task ProcessDiagramAsync(string inputPath, string outputPath)
    {
        // Load the diagram inside a using block as required.
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Collect all shape IDs from every page.
            List<long> shapeIds = new List<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shapeIds.Add(shape.ID);
                }
            }

            // Create a list of tasks to add geometry to each shape concurrently.
            List<Task> tasks = new List<Task>();
            foreach (long id in shapeIds)
            {
                tasks.Add(AddGeometryAsync(diagram, id));
            }

            // Await completion of all geometry-adding tasks.
            await Task.WhenAll(tasks);

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }

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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Run the asynchronous processing synchronously for the console entry point.
            Task processingTask = ProcessDiagramAsync(inputPath, outputPath);
            processingTask.GetAwaiter().GetResult();
            Console.WriteLine("Diagram processing completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}