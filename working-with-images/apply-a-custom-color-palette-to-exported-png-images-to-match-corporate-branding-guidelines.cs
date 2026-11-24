using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard for input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Define custom color palette mapping (original -> replacement)
        // Example: map corporate brand colors
        var colorMap = new Dictionary<Color, Color>
        {
            { FromHex("#FF0000"), FromHex("#003366") }, // Red -> Dark Blue
            { FromHex("#00FF00"), FromHex("#669900") }, // Lime -> Olive
            { FromHex("#0000FF"), FromHex("#CC6600") }  // Blue -> Orange
        };

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and export to PNG
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Configure image save options for a single page
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                imgOptions.PageIndex = pageIndex;
                imgOptions.PageCount = 1;
                imgOptions.Resolution = 300f; // High resolution

                // Build output PNG file path
                string pageName = diagram.Pages[pageIndex].Name;
                string pngPath = Path.Combine(outputFolder, $"{pageName}.png");

                // Export page to PNG
                diagram.Save(pngPath, imgOptions);

                // Apply custom palette to the exported PNG
                ApplyPalette(pngPath, colorMap);
            }

            Console.WriteLine("Export and palette application completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Loads a PNG, replaces colors according to the map, and saves it back
    private static void ApplyPalette(string imagePath, Dictionary<Color, Color> palette)
    {
        try
        {
            using (Bitmap bitmap = new Bitmap(imagePath))
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color original = bitmap.GetPixel(x, y);
                        if (palette.TryGetValue(original, out Color replacement))
                        {
                            bitmap.SetPixel(x, y, replacement);
                        }
                    }
                }

                // Overwrite the original PNG with the recolored version
                bitmap.Save(imagePath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to apply palette to {imagePath}: {ex.Message}");
        }
    }

    // Helper to convert a hex string (e.g., "#RRGGBB") to Aspose.Drawing.Color
    private static Color FromHex(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Invalid hex color.");

        hex = hex.TrimStart('#');
        if (hex.Length != 6)
            throw new ArgumentException("Hex color must be 6 characters.");

        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);
        return Color.FromArgb(r, g, b);
    }
}