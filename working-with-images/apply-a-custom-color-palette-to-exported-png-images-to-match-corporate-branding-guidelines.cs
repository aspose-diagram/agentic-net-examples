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

            // Load the source Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Create image save options for PNG format
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Apply corporate branding adjustments
            // Adjust brightness and contrast to fit the required palette
            pngOptions.ImageBrightness = 0.6f;   // value between 0 and 1
            pngOptions.ImageContrast   = 0.7f;   // value between 0 and 1

            // Ensure the image is saved in full color (no grayscale or B&W conversion)
            pngOptions.ImageColorMode = ImageColorMode.None;

            // Export each page of the diagram as a separate PNG file using the same options
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                string outputPath = $"Page_{pageIndex + 1}.png";
                diagram.Save(outputPath, pngOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
