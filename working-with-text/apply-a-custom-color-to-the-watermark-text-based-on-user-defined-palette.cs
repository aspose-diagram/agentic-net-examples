using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_with_watermark.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Define a user‑defined color palette (name -> HEX string)
                var palette = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Red",   "#FF0000" },
                    { "Green", "#00FF00" },
                    { "Blue",  "#0000FF" },
                    { "Gray",  "#808080" }
                };

                // Ask the user to choose a color name
                Console.WriteLine("Available colors: Red, Green, Blue, Gray");
                Console.Write("Enter watermark color name: ");
                string colorName = Console.ReadLine();

                // Resolve the color; fall back to Gray if not found
                if (!palette.TryGetValue(colorName ?? string.Empty, out string colorHex))
                {
                    Console.WriteLine("Color not recognized. Using default Gray.");
                    colorHex = "#808080";
                }

                // Add the watermark text covering the full page
                // Font size is specified in inches (e.g., 0.5 inches ≈ 36 points)
                Shape watermark = page.AddText(
                    pinX,               // PinX (center X)
                    pinY,               // PinY (center Y)
                    pageWidth,          // Width of the text box
                    pageHeight,         // Height of the text box
                    "CONFIDENTIAL",    // Watermark text
                    "Arial",            // Font name
                    colorHex,           // Font color (hex string)
                    0.5                 // Font size in inches
                );

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Watermark added with color {colorHex}. Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }