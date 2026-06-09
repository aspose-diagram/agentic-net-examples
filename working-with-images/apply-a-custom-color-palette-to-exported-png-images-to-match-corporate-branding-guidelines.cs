using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportDiagramWithCustomPalette
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram(@"C:\Diagrams\sample.vsdx");

            // Create image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Set color mode to None (full color). Adjust if grayscale or B&W is required.
            saveOptions.ImageColorMode = ImageColorMode.None;

            // Adjust brightness and contrast to align with corporate branding colors.
            // Values are between 0 and 1. Modify these as needed for your palette.
            saveOptions.ImageBrightness = 0.6f; // example brightness
            saveOptions.ImageContrast   = 0.7f; // example contrast

            // Export each page of the diagram as a separate PNG image
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Specify which page to render
                saveOptions.PageIndex = pageIndex;

                // Build output file name
                string outputPath = $@"C:\ExportedImages\Diagram_Page_{pageIndex + 1}.png";

                // Save the page as PNG using the configured options
                diagram.Save(outputPath, saveOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
