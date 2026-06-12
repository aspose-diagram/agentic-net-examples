using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);
            Page page = diagram.Pages[0];

            // Locate the two shapes by their universal names (adjust names as needed)
            long shapeId1 = FindShapeIdByName(page, "Shape1");
            long shapeId2 = FindShapeIdByName(page, "Shape2");

            if (shapeId1 == -1 || shapeId2 == -1)
            {
                Console.WriteLine("One or both shapes were not found.");
                return;
            }

            Shape shape1 = page.Shapes.GetShape(shapeId1);
            Shape shape2 = page.Shapes.GetShape(shapeId2);

            // Add a dynamic connector shape at an arbitrary position
            long connectorId = page.AddShape(0, 0, "Dynamic connector");

            // Determine the nearest connection points for each shape
            ConnectionPointPlace placeFrom = GetNearestConnectionPlace(shape1, shape2);
            ConnectionPointPlace placeTo = GetNearestConnectionPlace(shape2, shape1);

            // Connect the two shapes using the connector
            page.ConnectShapesViaConnector(shapeId1, placeFrom, shapeId2, placeTo, connectorId);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to find a shape's ID by its universal name; returns -1 if not found
    static long FindShapeIdByName(Page page, string nameU)
    {
        foreach (Shape shape in page.Shapes)
        {
            if (!string.IsNullOrEmpty(shape.NameU) &&
                shape.NameU.Equals(nameU, StringComparison.OrdinalIgnoreCase))
            {
                return shape.ID;
            }
        }
        return -1;
    }

    // Determines the nearest connection point based on relative position of two shapes
    static ConnectionPointPlace GetNearestConnectionPlace(Shape from, Shape to)
    {
        double dx = to.XForm.PinX.Value - from.XForm.PinX.Value;
        double dy = to.XForm.PinY.Value - from.XForm.PinY.Value;

        // Choose horizontal or vertical connection based on larger distance component
        if (Math.Abs(dx) > Math.Abs(dy))
        {
            return dx > 0 ? ConnectionPointPlace.Right : ConnectionPointPlace.Left;
        }
        else
        {
            return dy > 0 ? ConnectionPointPlace.Bottom : ConnectionPointPlace.Top;
        }
    }
}
