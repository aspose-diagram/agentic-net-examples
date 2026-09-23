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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram (VSDX format)
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Iterate through all pages and shapes to find background images
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Background images are foreign shapes that contain an Image object
                    if (shape.Type == TypeValue.Foreign && shape.Image != null)
                    {
                        // Apply a blur effect (value between 0.0 and 1.0)
                        shape.Image.Blur.Value = 0.25;
                    }
                }
            }

            // Configure PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the modified diagram to PNG
            string outputPath = "output.png";
            diagram.Save(outputPath, pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
