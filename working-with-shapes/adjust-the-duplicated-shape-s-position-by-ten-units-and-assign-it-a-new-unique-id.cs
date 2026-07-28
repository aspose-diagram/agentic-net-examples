using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust as needed)
            Page page = diagram.Pages[0];

            // Identify the shape to duplicate (example uses ID = 1)
            long originalShapeId = 1;
            Shape originalShape = page.Shapes.GetShape(originalShapeId);
            if (originalShape == null)
            {
                throw new Exception($"Shape with ID {originalShapeId} not found.");
            }

            // Retrieve master name, size and position of the original shape
            string masterName = originalShape.Master?.Name ?? "Rectangle";
            double width = originalShape.XForm.Width.Value;
            double height = originalShape.XForm.Height.Value;
            double pinX = originalShape.XForm.PinX.Value;
            double pinY = originalShape.XForm.PinY.Value;

            // Add a new shape using the same master and dimensions
            long newShapeId = page.AddShape(pinX, pinY, width, height, masterName);
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Adjust the duplicated shape's position by ten units (inches)
            newShape.XForm.PinX.Value = pinX + 10;
            newShape.XForm.PinY.Value = pinY + 10;

            // Ensure the new shape has a unique ID (greater than any existing ID on the page)
            long maxId = 0;
            foreach (Shape s in page.Shapes)
            {
                if (s.ID > maxId) maxId = s.ID;
            }
            newShape.ID = maxId + 1;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
