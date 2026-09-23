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

            // Path to the source Visio diagram
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Prepare image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Set the output image size to 100 pixels wide.
            // PageSize expects width and height in pixels.
            // Here we set both dimensions to 100 to create a square thumbnail.
            // Adjust the height proportionally if needed by calculating the aspect ratio.
            saveOptions.PageSize = new PageSize(100f, 100f);

            // Export the first page as a thumbnail PNG
            diagram.Save("thumbnail.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
