using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: inputVisioPath outputVisioPath opacityPercentage
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: WatermarkOpacityExample <inputVisioPath> <outputVisioPath> <opacityPercentage>");
                Console.WriteLine("Opacity must be between 10 and 90 (inclusive).");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            if (!int.TryParse(args[2], out int opacityPercent))
            {
                Console.WriteLine("Opacity must be an integer percentage.");
                return;
            }

            // Validate opacity range (10% - 90%)
            if (opacityPercent < 10 || opacityPercent > 90)
            {
                Console.WriteLine("Opacity percentage must be between 10 and 90.");
                return;
            }

            // Convert percentage to a decimal value (0.1 - 0.9) used by Visio transparency cells
            double opacityDecimal = opacityPercent / 100.0;

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Use the first page (or adjust as needed)
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Position the watermark at the center of the page
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Use the full page size for the watermark shape so the text spans the page
            double shapeWidth = pageWidth;
            double shapeHeight = pageHeight;

            // Watermark text and styling
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Calibri";
            string fontColorHex = "#A0A0A0"; // Light gray
            // Font size is in inches; 36 points ≈ 0.5 inches
            double fontSizeInches = 36.0 / 72.0;

            // Add the watermark shape
            Shape watermarkShape = page.AddText(pinX, pinY, shapeWidth, shapeHeight,
                                                watermarkText, fontName, fontColorHex, fontSizeInches);

            // Apply opacity via the FillForegndTrans cell (0 = opaque, 1 = fully transparent)
            // Since we want the watermark to be semi‑transparent, set the transparency to (1 - opacity)
            // Example: 70% opacity => 30% transparency
            double transparency = 1.0 - opacityDecimal;
            watermarkShape.Fill.FillForegndTrans.Value = transparency;

            // Optionally, reduce the shape's line visibility (no border)
            watermarkShape.Line.LineWeight.Value = 0.0;

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with watermark opacity {opacityPercent}% to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }