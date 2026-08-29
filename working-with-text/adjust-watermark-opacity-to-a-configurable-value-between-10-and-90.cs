using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Configurable opacity (10% to 90%). Value is between 0.1 (10%) and 0.9 (90%).
                double opacity = 0.5; // Example: 50% opacity

                // Validate the opacity range
                if (opacity < 0.1 || opacity > 0.9)
                    throw new Exception("Opacity must be between 0.1 (10%) and 0.9 (90%).");

                // Get the first page (you can iterate if needed)
                Page page = diagram.Pages[0];

                // Page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a full‑page text shape that will act as the watermark
                // Font size is expressed in inches (e.g., 0.5 inches ≈ 36 points)
                Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                    "CONFIDENTIAL", "Arial", "#CCCCCC", 0.5);

                // Apply transparency to the shape's fill using the shadow foreground transparency cell.
                // The value is a fraction where 0.0 = opaque and 1.0 = fully transparent.
                watermarkShape.Fill.ShdwForegndTrans.Value = opacity;

                // Optionally, reduce the text weight to make the watermark lighter
                watermarkShape.Line.LineWeight.Value = 0.01; // thin line (mostly irrelevant for text)

                // Save the modified diagram as an image (PNG)
                string outputPath = "output.png";
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine("Watermark applied with opacity " + (opacity * 100) + "% and saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }