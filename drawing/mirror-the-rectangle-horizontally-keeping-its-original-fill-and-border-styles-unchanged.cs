using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Find the first rectangle shape on the page
            Shape rectangle = null;
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a master and check its name
                if (shape.Master != null && shape.Master.Name == "Rectangle")
                {
                    rectangle = shape;
                    break;
                }
            }

            if (rectangle == null)
            {
                throw new Exception("No rectangle shape found on the first page.");
            }

            // Mirror the rectangle horizontally by setting FlipX to True
            // This operation does not affect fill or line styles
            rectangle.XForm.FlipX.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
