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

            // Work with the active page
            Page page = diagram.ActivePage;

            // Locate the first shape that uses the "Diamond" master
            Shape originalDiamond = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Diamond")
                {
                    originalDiamond = shape;
                    break;
                }
            }

            if (originalDiamond == null)
            {
                throw new Exception("Diamond shape not found in the diagram.");
            }

            // Retrieve original shape geometry
            double originalPinX = originalDiamond.XForm.PinX.Value;
            double originalPinY = originalDiamond.XForm.PinY.Value;
            double originalWidth = originalDiamond.XForm.Width.Value;
            double originalHeight = originalDiamond.XForm.Height.Value;
            string masterName = originalDiamond.Master.Name;

            // Calculate new position (0.5 inches to the right)
            double newPinX = originalPinX + 0.5;
            double newPinY = originalPinY;

            // Add a duplicate of the diamond shape at the new location
            long newShapeId = page.AddShape(newPinX, newPinY, originalWidth, originalHeight, masterName);
            // Optional: retrieve the newly added shape if further manipulation is needed
            Shape duplicatedDiamond = page.Shapes.GetShape(newShapeId);

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
