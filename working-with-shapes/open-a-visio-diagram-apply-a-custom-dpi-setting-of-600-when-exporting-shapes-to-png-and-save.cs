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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options for PNG with 600 DPI resolution
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.Resolution = 600; // DPI setting

            // Export each shape on each page to a separate PNG file
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Create a unique file name for each shape
                    string fileName = $"Page{page.ID}_Shape{shape.ID}.png";

                    // Export the shape using the configured options
                    shape.ToImage(fileName, imgOptions);
                }
            }

            // No diagram saving required for this operation

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
