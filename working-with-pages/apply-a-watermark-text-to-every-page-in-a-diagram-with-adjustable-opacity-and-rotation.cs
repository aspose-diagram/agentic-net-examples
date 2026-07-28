using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // args[0] - input Visio file path
            // args[1] - output Visio file path
            // args[2] - watermark text
            // args[3] - opacity (0-100, where 0 = fully transparent, 100 = opaque)
            // args[4] - rotation in degrees

            if (args.Length < 5)
            {
                Console.WriteLine("Usage: DiagramWatermark <inputPath> <outputPath> <watermarkText> <opacity> <rotationDegrees>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string watermarkText = args[2];
            if (!double.TryParse(args[3], out double opacity) || opacity < 0 || opacity > 100)
            {
                Console.WriteLine("Invalid opacity value. Must be a number between 0 and 100.");
                return;
            }
            if (!double.TryParse(args[4], out double rotationDegrees))
            {
                Console.WriteLine("Invalid rotation value.");
                return;
            }

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add the watermark
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Use the full page size for the text box so the text can be centered easily
                double textBoxWidth = pageWidth;
                double textBoxHeight = pageHeight;

                // Add the text shape with desired font properties
                // Font size is specified in inches (points / 72)
                double fontSizeInPoints = 72; // 1 inch = 72 points
                double fontSizeInInches = fontSizeInPoints / 72.0;

                Shape watermarkShape = page.AddText(
                    pinX,
                    pinY,
                    textBoxWidth,
                    textBoxHeight,
                    watermarkText,
                    "Calibri",          // Font name
                    "#808080",          // Font color (gray)
                    fontSizeInInches    // Font size in inches
                );

                // Set the rotation (TextXForm uses radians)
                watermarkShape.TextXForm.TxtAngle.Value = (float)(Math.PI / 180.0 * rotationDegrees);

                // Apply opacity to the shape's fill (percentage)
                // This makes the entire shape (including text) semi‑transparent
                watermarkShape.Fill.FillForegndTrans.Value = opacity;

                // Optionally, reduce the fill opacity to make the text stand out less
                // and set the fill color to transparent (no background)
                watermarkShape.Fill.FillForegnd.Value = "#FFFFFF"; // White fill (will be transparent due to opacity)
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }