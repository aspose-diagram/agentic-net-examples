using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page; create one if necessary.
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Access the first page.
                Page page = diagram.Pages[0];

                // -----------------------------------------------------------------
                // Add two rectangle shapes that will be grouped.
                // -----------------------------------------------------------------
                long rect1Id = page.AddShape(1.0, 1.0, 2.0, 1.0, "Rectangle", false);
                long rect2Id = page.AddShape(4.0, 1.0, 2.0, 1.0, "Rectangle", false);

                // Retrieve the Shape objects for grouping.
                Shape rect1 = page.Shapes.GetShape(rect1Id);
                Shape rect2 = page.Shapes.GetShape(rect2Id);

                // -----------------------------------------------------------------
                // Group the two rectangles into a single group shape.
                // -----------------------------------------------------------------
                Shape groupShape = page.Shapes.Group(new Shape[] { rect1, rect2 });

                // -----------------------------------------------------------------
                // Enable dynamic gluing on the group shape.
                // -----------------------------------------------------------------
                // Allow connectors to glue to this group.
                groupShape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

                // -----------------------------------------------------------------
                // Add a dynamic connector shape.
                // -----------------------------------------------------------------
                long connectorId = page.AddShape(0.0, 0.0, 0.0, 0.0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // -----------------------------------------------------------------
                // Connect the connector: start glued to the group, end glued to the first rectangle.
                // -----------------------------------------------------------------
                page.ConnectShapesViaConnector(
                    groupShape.ID, ConnectionPointPlace.Bottom,
                    rect1Id, ConnectionPointPlace.Top,
                    connectorId);

                // -----------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}