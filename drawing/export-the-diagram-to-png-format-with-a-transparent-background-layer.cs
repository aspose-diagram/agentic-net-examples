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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Configure image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.SaveFormat = SaveFileFormat.Png; // Explicitly set PNG format

            // Set transparent background if the option is available
            // (Aspose.Diagram uses transparent background for PNG by default;
            // if a BackgroundColor property exists, it can be set to Color.Transparent)
            // saveOptions.BackgroundColor = Color.Transparent; // Uncomment if supported

            // Save the diagram as a PNG image with the specified options
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
