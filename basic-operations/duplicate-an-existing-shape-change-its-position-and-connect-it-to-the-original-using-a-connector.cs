using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page of the diagram
            Page page = diagram.Pages[0];

            // Find the first shape on the page to duplicate
            Shape originalShape = null;
            foreach (Shape s in page.Shapes)
            {
                originalShape = s;
                break;
            }

            if (originalShape == null)
            {
                Console.WriteLine("No shape found on the page to duplicate.");
                return;
            }

            // Store the original shape's ID
            long originalShapeId = originalShape.ID;

            // Determine the master name of the original shape (fallback to a basic master if null)
            string masterName = originalShape.Master != null ? originalShape.Master.Name : "Rectangle";

            // Define new position offset (in inches)
            double offsetX = 2.0;
            double offsetY = 2.0;

            // Add a duplicate shape using the same master at the new position
            double newPinX = originalShape.XForm.PinX.Value + offsetX;
            double newPinY = originalShape.XForm.PinY.Value + offsetY;
            long newShapeId = page.AddShape(newPinX, newPinY, masterName);

            // Retrieve the newly added shape (optional, for further adjustments)
            Shape newShape = page.Shapes.GetShape(newShapeId);
            newShape.XForm.PinX.Value = newPinX;
            newShape.XForm.PinY.Value = newPinY;

            // Add a dynamic connector shape (position does not matter; it will be routed automatically)
            long connectorId = page.AddShape(0, 0, "Dynamic connector");
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect the original shape to the duplicated shape using the connector
            page.ConnectShapesViaConnector(
                originalShapeId,
                ConnectionPointPlace.Bottom,
                newShapeId,
                ConnectionPointPlace.Top,
                connectorId);

            // Optionally set connector routing style (e.g., right‑angle)
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Shape duplicated, repositioned, and connected successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
