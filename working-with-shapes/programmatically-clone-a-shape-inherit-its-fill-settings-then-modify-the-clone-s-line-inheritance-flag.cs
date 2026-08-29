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

            // Load an existing Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page in the document
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page to serve as the source for cloning
            Shape originalShape = null;
            foreach (Shape s in page.Shapes)
            {
                originalShape = s;
                break;
            }

            if (originalShape == null)
                throw new Exception("The page does not contain any shapes to clone.");

            // Determine the master name of the original shape (fallback to a basic master if null)
            string masterName = originalShape.Master != null ? originalShape.Master.Name : "Rectangle";

            // Position the cloned shape slightly offset from the original
            double newPinX = originalShape.XForm.PinX.Value + 2.0; // shift 2 inches on X axis
            double newPinY = originalShape.XForm.PinY.Value;      // same Y position

            // Add a new shape on the page using the same master
            long cloneShapeIdLong = page.AddShape(newPinX, newPinY, masterName);
            Shape cloneShape = page.Shapes.GetShape((int)cloneShapeIdLong);

            // Copy formatting and geometry from the original shape to the clone
            cloneShape.Copy(originalShape);

            // Ensure the clone inherits fill settings (clear any local fill overrides)
            // Setting FillStyle to null forces inheritance from the master/style
            cloneShape.FillStyle = null;

            // Modify the clone's line inheritance flag by overriding the line color
            // This breaks line inheritance and applies a custom line color (red)
            cloneShape.Line.LineColor.Value = "#FF0000";

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
