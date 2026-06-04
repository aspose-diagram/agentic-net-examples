using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.jpg";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Add a full‑page watermark text shape
            // Font size is specified in inches (0.5 in ≈ 36 pt)
            Shape watermark = page.AddText(
                0,
                0,
                pageWidth,
                pageHeight,
                "CONFIDENTIAL",
                "Calibri",
                "#A5A5A5",
                0.5);

            // Send the watermark to the back so it doesn't obscure content
            watermark.SendToBack();

            // Configure JPEG export with 150 DPI
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
            {
                Resolution = 150,          // DPI
                ExportHiddenPage = false, // Do not export hidden pages
                PageIndex = 0             // Export the first page
            };

            // Save the diagram as a JPEG image
            diagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}