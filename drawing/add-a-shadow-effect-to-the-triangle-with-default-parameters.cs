using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assume the triangle is on the first page
            Page page = diagram.Pages[0];

            // Find the shape whose master name is "Triangle"
            Shape triangleShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Triangle")
                {
                    triangleShape = shape;
                    break;
                }
            }

            if (triangleShape == null)
            {
                Console.WriteLine("Triangle shape not found.");
                return;
            }

            // Apply default shadow effect using cell-based API
            // Simple shadow type
            triangleShape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
            // Shadow color (gray)
            triangleShape.Fill.ShdwForegnd.Value = "#808080";
            // Shadow transparency (30% transparent)
            triangleShape.Fill.ShdwForegndTrans.Value = 0.3;
            // Shadow offsets (default small offsets)
            triangleShape.Fill.ShapeShdwOffsetX.Value = 0.1;
            triangleShape.Fill.ShapeShdwOffsetY.Value = 0.1;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with shadow applied to triangle at '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
