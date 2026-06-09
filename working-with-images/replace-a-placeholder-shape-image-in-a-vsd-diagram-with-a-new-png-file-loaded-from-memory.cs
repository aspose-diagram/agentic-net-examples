using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output Visio file path
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // PNG image data loaded into memory (replace with actual byte array as needed)
            byte[] pngData = File.ReadAllBytes("newImage.png");

            // Find the placeholder shape (first foreign shape in the first page)
            Page page = diagram.Pages[0];
            Shape placeholderShape = null;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Foreign)
                {
                    placeholderShape = shape;
                    break;
                }
            }

            if (placeholderShape == null)
            {
                throw new Exception("Placeholder shape not found.");
            }

            // Replace the image data
            placeholderShape.ForeignData.Value = pngData;

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
