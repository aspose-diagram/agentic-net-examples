using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add a full‑page watermark
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a text shape that covers the entire page.
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                page.AddText(0, 0, pageWidth, pageHeight,
                             "CONFIDENTIAL", "Calibri", "#A0A0A0", 0.25);
            }

            // Save the modified diagram
            string outputPath = "output_watermarked.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
