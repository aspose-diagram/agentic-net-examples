using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSDX diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure high‑quality image export options
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Example: set a high resolution if the option is available
            // imgOptions.Resolution = 300;

            // Create a temporary folder to store extracted shape images
            string tempFolder = Path.Combine(Path.GetTempPath(), "DiagramImages");
            Directory.CreateDirectory(tempFolder);

            int shapeCounter = 0;
            // Iterate through all pages and shapes to extract images
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    string imagePath = Path.Combine(tempFolder, $"shape_{shapeCounter++}.png");
                    shape.ToImage(imagePath, imgOptions);
                }
            }

            // Save the entire diagram as a PDF; extracted images are embedded automatically
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
