using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramWatermark <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add a semi‑transparent watermark.
            foreach (Page page in diagram.Pages)
            {
                // Page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark.
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a full‑page text shape as the watermark.
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches).
                Shape watermark = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                              "CONFIDENTIAL",
                                              "Calibri",
                                              "#A0A0A0",          // Light gray color.
                                              0.25);               // Font size ~18pt (18/72).

                // Rotate the watermark for a typical diagonal appearance.
                watermark.XForm.Angle.Value = 45.0;

                // Apply transparency to both fill (background) and line (border) of the shape.
                // Transparency values are percentages (0‑100).
                watermark.Fill.FillForegndTrans.Value = 80;   // 80 % transparent fill.
                watermark.Line.LineColorTrans.Value = 80;    // 80 % transparent line.

                // Optionally hide the shape border by setting line weight to zero.
                watermark.Line.LineWeight.Value = 0.0;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }