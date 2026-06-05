using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.Drawing;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options for PNG format
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
            // PNG supports transparency by default; if the library exposes a background color property,
            // it can be set to Color.Transparent here.
            // options.BackgroundColor = Color.Transparent; // Uncomment if the property exists

            // Export the diagram to PNG with a transparent background
            diagram.Save("output.png", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
