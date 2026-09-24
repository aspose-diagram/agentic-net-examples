using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output Visio file path
            string outputPath = "output.vsdx";

            // Desired dimensions (in inches)
            double newWidth = 2.0;
            double newHeight = 1.0;

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    targetShape = shape;
                    break;
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shape found on the page.");
                    return;
                }

                // Resize the shape
                targetShape.XForm.Width.Value = newWidth;
                targetShape.XForm.Height.Value = newHeight;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Shape resized and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
