using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

using System.Drawing; // For Color if needed

class ExportDiagramToPng
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options for PNG format
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.SaveFormat = SaveFileFormat.Png;          // Explicitly set PNG format
            // If the library exposes a BackgroundColor property, set it to transparent.
            // Uncomment the following line if such a property exists:
            // pngOptions.BackgroundColor = Color.Transparent;

            // Save the diagram as a PNG image with the specified options
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
