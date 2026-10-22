using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define a simple palette of color names to hex values
        var palette = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Red",    "#FF0000" },
            { "Green",  "#00FF00" },
            { "Blue",   "#0000FF" },
            { "Gray",   "#808080" },
            { "Custom", "#000000" } // placeholder for user‑entered hex
        };

        // Prompt the user to choose a color
        Console.WriteLine("Select a watermark color from the palette:");
        foreach (var kvp in palette)
        {
            Console.WriteLine($"- {kvp.Key}");
        }
        Console.Write("Enter color name (or hex value starting with #): ");
        string userChoice = Console.ReadLine()?.Trim();

        string selectedColorHex;

        if (string.IsNullOrEmpty(userChoice))
        {
            Console.Error.WriteLine("No color selected.");
            return;
        }
        else if (palette.ContainsKey(userChoice))
        {
            // Use predefined palette entry
            selectedColorHex = palette[userChoice];
        }
        else if (userChoice.StartsWith("#") && userChoice.Length == 7)
        {
            // Assume user entered a valid hex string
            selectedColorHex = userChoice;
        }
        else
        {
            Console.Error.WriteLine("Invalid color selection.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and add a full‑page watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Use full page dimensions for the watermark background
                    double watermarkWidth = pageWidth;
                    double watermarkHeight = pageHeight;

                    // Center the watermark (pinX and pinY represent the lower‑left corner)
                    double pinX = (pageWidth - watermarkWidth) / 2.0;
                    double pinY = (pageHeight - watermarkHeight) / 2.0;

                    // Watermark text and styling
                    string watermarkText = "CONFIDENTIAL";
                    string fontName = "Calibri";
                    double fontSize = 0.25; // Visio uses inches for font size

                    // Add the watermark text to the page
                    page.AddText(pinX, pinY, watermarkWidth, watermarkHeight,
                                 watermarkText, fontName, selectedColorHex, fontSize);
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}