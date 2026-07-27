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
            string inputPath = "input.vsdx"; // replace with actual file path
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page
            Page page = diagram.Pages[0];

            // Find the first diamond shape on the page
            Shape? diamondShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a master and that the master name is "Diamond"
                if (shape.Master != null && shape.Master.Name == "Diamond")
                {
                    diamondShape = shape;
                    break;
                }
            }

            if (diamondShape == null)
            {
                Console.WriteLine("No diamond shape found on the page.");
                return;
            }

            // Retrieve original position
            double originalPinX = diamondShape.XForm.PinX.Value;
            double originalPinY = diamondShape.XForm.PinY.Value;

            // Calculate new position (offset 0.5 inches to the right)
            double newPinX = originalPinX + 0.5;
            double newPinY = originalPinY; // Y coordinate remains the same

            // Duplicate the shape using the same master name
            string masterName = diamondShape.Master.Name;
            long newShapeId = page.AddShape(newPinX, newPinY, masterName);

            // Optionally retrieve the newly added shape for further modifications
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Save the modified diagram
            string outputPath = "output.vsdx"; // replace with desired output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diamond shape duplicated and offset successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
