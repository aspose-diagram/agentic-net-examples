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

            // Load the VSD diagram
            Diagram diagram = new Diagram("input.vsd");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape contains an image (background picture), apply blur
                    if (shape.Image != null)
                    {
                        // Blur value must be between 0 and 1; here we set it to 0.5 (50% blur)
                        shape.Image.Blur.Value = 0.5;
                    }
                }
            }

            // Prepare PNG save options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the diagram (all pages) to a PNG file
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
