using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the rectangle shape by its master name
            Shape rectangle = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Rectangle")
                {
                    rectangle = shape;
                    break;
                }
            }

            if (rectangle == null)
            {
                Console.WriteLine("Rectangle shape not found.");
                return;
            }

            // Retrieve original dimensions
            double originalWidth = rectangle.XForm.Width.Value;
            double originalHeight = rectangle.XForm.Height.Value;

            // Scale width and height proportionally (double size)
            rectangle.XForm.Width.Value = originalWidth * 2;
            rectangle.XForm.Height.Value = originalHeight * 2;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Rectangle scaled and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
