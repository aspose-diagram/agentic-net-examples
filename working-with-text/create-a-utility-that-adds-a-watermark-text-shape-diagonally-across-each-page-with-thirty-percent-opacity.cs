using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output file path.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: WatermarkUtility <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center coordinates.
                    double centerX = pageWidth / 2.0;
                    double centerY = pageHeight / 2.0;

                    // Add a full‑page text shape that will serve as the watermark.
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize(in inches).
                    Shape watermark = page.AddText(
                        centerX,               // pinX (center of rotation)
                        centerY,               // pinY (center of rotation)
                        pageWidth,             // width of the text box
                        pageHeight,            // height of the text box
                        "CONFIDENTIAL",        // watermark text
                        "Arial",               // font name
                        "#808080",             // font color (gray)
                        1.0                    // font size (72 pt = 1 inch)
                    );

                    // Rotate the text 45 degrees (diagonal across the page).
                    // TextXForm.TxtAngle expects radians.
                    watermark.TextXForm.TxtAngle.Value = (Math.PI / 180.0) * 45.0;

                    // Set the shape's fill transparency to achieve 30 % opacity.
                    // Transparency value: 0 = opaque, 100 = fully transparent.
                    // 30 % opacity => 70 % transparency.
                    watermark.Fill.FillForegndTrans.Value = 70.0;

                    // Ensure the shape has no background fill (optional, keeps only the text visible).
                    watermark.Fill.FillPattern.Value = 0;
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Watermark added and diagram saved to '{outputPath}'.");
        }
    }