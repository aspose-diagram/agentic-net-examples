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

            // Paths – adjust as needed
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            var diagram = new Diagram(inputPath);
            var page = diagram.Pages[0];

            // Retrieve the first shape on the page (original shape)
            Shape originalShape = null;
            foreach (Shape s in page.Shapes)
            {
                originalShape = s;
                break;
            }
            if (originalShape == null)
                throw new Exception("No shape found on the page.");

            // Ensure the original shape has a master (required for duplication)
            if (originalShape.Master == null)
                throw new Exception("Original shape does not have an associated master.");

            // Duplicate the shape using the same master name
            string masterName = originalShape.Master.Name;
            double offsetX = 2.0; // shift duplicated shape 2 inches to the right
            double newPinX = originalShape.XForm.PinX.Value + offsetX;
            double newPinY = originalShape.XForm.PinY.Value; // keep same Y

            long newShapeId = page.AddShape(newPinX, newPinY, masterName);
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // (Optional) Adjust any additional properties of the duplicated shape here
            // Example: change fill color
            // newShape.Fill.FillForegnd.Value = "#FFCC00";

            // Create a dynamic connector shape
            long connectorId = page.AddShape(0, 0, "Dynamic connector");
            Shape connector = page.Shapes.GetShape(connectorId);

            // Set connector routing style (right‑angle)
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Connect original shape (bottom) to duplicated shape (top) using the connector
            page.ConnectShapesViaConnector(
                originalShape.ID,
                ConnectionPointPlace.Bottom,
                newShape.ID,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
