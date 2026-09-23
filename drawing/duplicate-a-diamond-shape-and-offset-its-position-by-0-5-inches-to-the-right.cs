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

            // Work with the first page
            Page page = diagram.Pages[0];

            // Locate the first diamond shape on the page
            Shape? originalShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Diamond")
                {
                    originalShape = shape;
                    break;
                }
            }

            if (originalShape == null)
            {
                Console.WriteLine("No diamond shape found on the page.");
                return;
            }

            // Retrieve the master name and original position
            string masterName = originalShape.Master.Name;
            double origPinX = originalShape.XForm.PinX.Value;
            double origPinY = originalShape.XForm.PinY.Value;

            // Add a new shape using the same master and offset its X position by 0.5 inches
            long newShapeId = page.AddShape(origPinX + 0.5, origPinY, masterName, false);

            // Optionally retrieve the newly added shape (not required for the offset operation)
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
