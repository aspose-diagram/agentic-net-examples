using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Find the first non-deleted shape on the page
            Shape originalShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.False) // skip deleted shapes
                {
                    originalShape = shp;
                    break;
                }
            }

            if (originalShape == null)
            {
                Console.WriteLine("No shape found to clone.");
                return;
            }

            // Retrieve master name, position and size of the original shape
            string masterName = originalShape.Master != null ? originalShape.Master.Name : "Rectangle";
            double pinX = originalShape.XForm.PinX.Value;
            double pinY = originalShape.XForm.PinY.Value;
            double width = originalShape.XForm.Width.Value;
            double height = originalShape.XForm.Height.Value;

            // Define new position (offset by 2 inches on the X axis)
            double newPinX = pinX + 2.0;
            double newPinY = pinY;

            // Add a new shape using the same master and size, positioned at the new location
            long newShapeId = page.AddShape(newPinX, newPinY, width, height, masterName, false);
            Shape clonedShape = page.Shapes.GetShape(newShapeId);

            // Copy plain text from the original shape to the cloned shape
            string plainText = originalShape.Text.Value.ToString();
            clonedShape.Text.Value.Clear();
            clonedShape.Text.Value.Add(new Txt(plainText));

            // Optionally, copy other visual properties (fill, line) if needed
            clonedShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
            clonedShape.Line.LineColor.Value = originalShape.Line.LineColor.Value;
            clonedShape.Line.LineWeight.Value = originalShape.Line.LineWeight.Value;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
