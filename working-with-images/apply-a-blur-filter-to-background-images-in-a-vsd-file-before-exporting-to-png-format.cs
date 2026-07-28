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

            // Apply blur to every shape that contains an image (typically background images)
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Image != null)
                    {
                        // Blur value must be between 0 and 1; here we set it to 0.5 (50% blur)
                        shape.Image.Blur.Value = 0.5;
                    }
                }
            }

            // Prepare PNG save options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the diagram (first page) to PNG format
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
