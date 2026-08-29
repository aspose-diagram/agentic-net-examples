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

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Configure image save options for PNG with a DPI of 600
            var imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.Resolution = 600;               // Set custom DPI
            imgOptions.SaveFormat = SaveFileFormat.Png; // Ensure PNG format

            // Export every shape on every page to a PNG file using the DPI setting
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    string fileName = $"Page{page.ID}_Shape{shape.ID}.png";
                    shape.ToImage(fileName, imgOptions);
                }
            }

            // Save the (unchanged) diagram back to a file if needed
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
