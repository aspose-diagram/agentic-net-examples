using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file with watermark
                string outputPath = "output_with_watermark.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Choose the page to which the watermark will be added (first page in this example)
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Watermark text and styling
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Arial";
                string fontColor = "#CCCCCC"; // Light gray in hex
                double fontSizePoints = 72;   // 72 points = 1 inch
                double fontSizeInches = fontSizePoints / 72.0;

                // Add the watermark as a full‑page text shape.
                // The AddText overload returns a Shape object that can be further customized if needed.
                Shape watermarkShape = page.AddText(
                    pinX,                // PinX (center X)
                    pinY,                // PinY (center Y)
                    pageWidth,           // Width (cover full page)
                    pageHeight,          // Height (cover full page)
                    watermarkText,       // Text content
                    fontName,            // Font name
                    fontColor,           // Font color (hex string)
                    fontSizeInches);     // Font size (in inches)

                // OPTIONAL: Rotate the watermark for a diagonal effect.
                // Rotation angle is in radians. 45 degrees = Math.PI / 4.
                watermarkShape.SetAngle(Math.PI / 4);

                // OPTIONAL: Reduce opacity by setting the shape's fill transparency.
                // Transparency is a percentage (0‑100). Here we set 50% transparency.
                watermarkShape.Fill.FillForegndTrans.Value = 50;

                // Save the modified diagram back to Visio format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Additionally, export the diagram as a PNG image to verify the watermark visually.
                string pngPath = "output_with_watermark.png";
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(pngPath, pngOptions);

                Console.WriteLine("Watermark added and files saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }