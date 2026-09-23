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

            // Load the Visio diagram (VSDX format)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Configure image save options for TIFF with grayscale conversion
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                saveOptions.ImageColorMode = ImageColorMode.Grayscale; // Apply grayscale filter
                saveOptions.PageIndex = page.ID; // Export the current page only

                // Define output file name per page
                string outputPath = $"Page_{page.ID}.tiff";

                // Save the page as a TIFF image
                diagram.Save(outputPath, saveOptions);
            }

            // No explicit disposal needed; objects will be cleaned up by the runtime

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
