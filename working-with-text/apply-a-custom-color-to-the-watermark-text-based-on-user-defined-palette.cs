using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_with_watermark.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define a user‑defined palette of hex color strings
        string[] palette = new string[]
        {
            "#FF0000", // Red
            "#00FF00", // Green
            "#0000FF", // Blue
            "#FFFF00"  // Yellow
        };

        // Prompt the user to select a color index
        Console.WriteLine($"Select a watermark color (0-{palette.Length - 1}):");
        string input = Console.ReadLine();
        int colorIndex;
        if (!int.TryParse(input, out colorIndex) || colorIndex < 0 || colorIndex >= palette.Length)
        {
            Console.WriteLine("Invalid selection. Defaulting to index 0.");
            colorIndex = 0;
        }

        string selectedColor = palette[colorIndex];

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page and add a full‑page watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add watermark text covering the full page
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize
                    page.AddText(pinX, pinY, pageWidth, pageHeight,
                                 "CONFIDENTIAL", "Calibri", selectedColor, 0.25);
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Watermark applied with color {selectedColor} and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}