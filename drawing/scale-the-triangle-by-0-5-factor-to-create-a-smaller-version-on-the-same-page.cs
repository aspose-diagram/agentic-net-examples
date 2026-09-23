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

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Scaling factor for the triangle
            double scaleFactor = 0.5;

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Find shapes that use the "Triangle" master and scale them
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Triangle")
                {
                    // Reduce width and height by the scaling factor
                    shape.XForm.Width.Value *= scaleFactor;
                    shape.XForm.Height.Value *= scaleFactor;

                    // PinX and PinY represent the shape's center; shrinking around the center requires no further adjustment
                }
            }

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
