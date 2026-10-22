using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static async Task Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
                // Process all shapes asynchronously
                await ProcessDiagramAsync(diagram);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Asynchronously adds a sample geometry to every shape in the diagram.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private static async Task ProcessDiagramAsync(Diagram diagram)
    {
        // Collect tasks for each shape
        List<Task> geometryTasks = new List<Task>();

        // Iterate over pages explicitly
        foreach (Page page in diagram.Pages)
        {
            // Iterate over shapes explicitly
            foreach (Shape shape in page.Shapes)
            {
                // Create a geometry instance to add
                Geom geom = CreateSampleGeom();

                // Queue the asynchronous addition
                geometryTasks.Add(AddGeometryToShapeAsync(shape, geom));
            }
        }

        // Await all geometry addition tasks
        await Task.WhenAll(geometryTasks);
    }

    /// <summary>
    /// Creates a simple geometry consisting of a MoveTo and a LineTo row.
    /// </summary>
    /// <returns>A Geom object ready to be added to a shape.</returns>
    private static Geom CreateSampleGeom()
    {
        // Initialize a new Geom collection
        Geom geom = new Geom();

        // Add a MoveTo row (starting point)
        MoveTo moveTo = new MoveTo
        {
            // Assign numeric values via the DoubleValue's Value property
            X = { Value = 0.0 },
            Y = { Value = 0.0 }
        };
        geom.CoordinateCol.Add(moveTo);

        // Add a LineTo row (draw a line to the right)
        LineTo lineTo = new LineTo
        {
            X = { Value = 1.0 },
            Y = { Value = 0.0 }
        };
        geom.CoordinateCol.Add(lineTo);

        return geom;
    }

    /// <summary>
    /// Asynchronously adds the provided geometry to the specified shape.
    /// </summary>
    /// <param name="shape">The target Shape.</param>
    /// <param name="geom">The Geom to add.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    private static async Task AddGeometryToShapeAsync(Shape shape, Geom geom)
    {
        // Wrap the synchronous Aspose operation in Task.Run to avoid blocking
        await Task.Run(() =>
        {
            try
            {
                // Add the geometry to the shape's Geoms collection
                shape.Geoms.Add(geom);
            }
            catch (Exception ex)
            {
                // Log any errors related to a specific shape
                Console.Error.WriteLine($"Failed to add geometry to shape ID {shape.ID}: {ex.Message}");
            }
        });
    }
}
