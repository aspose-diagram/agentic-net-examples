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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page
            Page page = diagram.Pages[0];

            // Locate a non‑connector shape to duplicate
            Shape originalShape = null;
            foreach (Shape s in page.Shapes)
            {
                if (!s.OneD) // ensure it's not a 1‑D connector
                {
                    originalShape = s;
                    break;
                }
            }

            if (originalShape == null)
            {
                Console.WriteLine("No suitable shape found to duplicate.");
                return;
            }

            // Duplicate the shape using its master name
            long duplicatedShapeId = page.AddShape(
                originalShape.XForm.PinX.Value,
                originalShape.XForm.PinY.Value,
                originalShape.Master.Name,
                false);
            Shape duplicatedShape = page.Shapes.GetShape(duplicatedShapeId);

            // Reposition the duplicated shape (e.g., 2 inches to the right)
            duplicatedShape.XForm.PinX.Value = originalShape.XForm.PinX.Value + 2.0;
            duplicatedShape.XForm.PinY.Value = originalShape.XForm.PinY.Value;

            // Add a dynamic connector shape
            long connectorId = page.AddShape(0, 0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);
            // Optional: set connector routing style
            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

            // Connect the original shape (bottom) to the duplicated shape (top) using the connector
            page.ConnectShapesViaConnector(
                originalShape.ID,
                ConnectionPointPlace.Bottom,
                duplicatedShape.ID,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Shape duplicated, repositioned, and connected successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
