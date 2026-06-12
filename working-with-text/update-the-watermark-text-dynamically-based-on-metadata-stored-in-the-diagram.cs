using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine watermark text from custom document properties
            string watermarkText = "Default Watermark";

            if (diagram.DocumentProps.CustomProps.Count > 0)
            {
                // Use the first custom property value as the watermark text
                var customProp = diagram.DocumentProps.CustomProps[0];
                if (!string.IsNullOrEmpty(customProp.CustomValue.ValueString))
                {
                    watermarkText = customProp.CustomValue.ValueString;
                }
            }

            // Add or update watermark on each page
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a full‑page text shape as watermark
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                page.AddText(0, 0, pageWidth, pageHeight, watermarkText, "Arial", "#808080", 0.5);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Watermark updated and diagram saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
