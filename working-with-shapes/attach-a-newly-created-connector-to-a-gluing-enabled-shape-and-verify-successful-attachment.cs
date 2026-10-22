using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "GluedDiagram.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the active page (creates one if none exists)
            Page page = diagram.ActivePage;

            // Add a rectangle shape (gluing-enabled) to the page
            // Parameters: pinX, pinY, masterName, isCalculate
            long rectangleId = page.AddShape(2.0, 2.0, "Rectangle", false);
            Shape rectangleShape = page.Shapes.GetShape(rectangleId);

            // Add a dynamic connector shape to the page
            long connectorId = page.AddShape(3.0, 3.0, "Dynamic connector", false);
            Shape connectorShape = page.Shapes.GetShape(connectorId);

            // Glue the connector to the rectangle using the center connection point
            // shape1_ID = rectangle, shape2_ID = connector
            page.GlueShapes(rectangleId, ConnectionPointPlace.Center, connectorId);

            // Verify that the connector is glued to the rectangle
            // Retrieve all 1-D glued shapes (connectors) from the rectangle
            long[] gluedShapeIds = rectangleShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

            bool isGlued = false;
            if (gluedShapeIds != null)
            {
                foreach (long id in gluedShapeIds)
                {
                    if (id == connectorId)
                    {
                        isGlued = true;
                        break;
                    }
                }
            }

            if (!isGlued)
            {
                throw new Exception("Connector was not successfully glued to the rectangle shape.");
            }
            else
            {
                Console.WriteLine("Connector successfully glued to the rectangle shape.");
            }

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}