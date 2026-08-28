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

            // Load the VSD file
            Diagram diagram = new Diagram("input.vsd");

            // Apply blur to all shapes that contain an image (background pictures)
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Image != null)
                    {
                        // Blur value is between 0 (no blur) and 1 (maximum blur)
                        shape.Image.Blur.Value = 0.5; // 50% blur
                    }
                }
            }

            // Prepare PNG save options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the diagram to PNG format
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
