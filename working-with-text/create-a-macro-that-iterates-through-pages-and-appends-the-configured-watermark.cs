using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Watermark configuration
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Calibri";
                string fontColor = "#A5A5A5"; // Light gray
                double fontSizeInInches = 0.5; // Approx. 36 points

                // Iterate through all pages and add the watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Add a text shape that covers the entire page
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize
                    page.AddText(0, 0, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSizeInInches);
                }

                // Save the modified diagram
                string outputPath = "output_with_watermark.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Watermark applied to all pages and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
