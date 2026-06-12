using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_with_watermark.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and add a diagonal watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark text shape
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a full‑page text shape as the watermark
                    // Font size is specified in inches (e.g., 0.5 inches ≈ 36 points)
                    Shape watermark = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                  "CONFIDENTIAL", "Arial", "#808080", 0.5);

                    // Rotate the text 45 degrees to make it diagonal
                    watermark.TextXForm.TxtAngle.Value = (float)(Math.PI / 180.0 * 45.0);

                    // Set fill transparency to 70 % (i.e., 30 % opacity)
                    // This makes the watermark semi‑transparent
                    watermark.Fill.FillForegndTrans.Value = 70.0;
                    // Optional: set a fill color (white) so the transparency applies to a visible background
                    watermark.Fill.FillForegnd.Value = "#FFFFFF";
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }