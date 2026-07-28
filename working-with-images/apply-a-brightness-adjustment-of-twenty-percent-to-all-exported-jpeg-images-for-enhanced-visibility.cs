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

            // Load the source diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare image save options for JPEG format
            ImageSaveOptions jpegOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);

            // Increase brightness by 20% (default is 0.5, so set to 0.7)
            jpegOptions.ImageBrightness = 0.7f;

            // Export each page as a separate JPEG with the adjusted brightness
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                jpegOptions.PageIndex = pageIndex;   // select current page
                jpegOptions.PageCount = 1;           // export only this page

                string outputFile = $"output_page_{pageIndex + 1}.jpg";
                diagram.Save(outputFile, jpegOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
