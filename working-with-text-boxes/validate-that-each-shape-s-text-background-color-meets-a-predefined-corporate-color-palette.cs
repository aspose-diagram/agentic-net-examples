using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Define the corporate palette as a list of allowed RGB tuples
            var allowedPalette = new List<(int R, int G, int B)>
            {
                (255, 0, 0),   // Red
                (0, 255, 0),   // Green
                (0, 0, 255),   // Blue
                (95, 108, 53)  // Example corporate color
            };

            // Iterate through every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the text background color formula (e.g., "RGB(95,108,53)")
                    string bgFormula = shape.TextBlock?.TextBkgnd?.Ufe?.F;

                    // Skip shapes without a text background definition
                    if (string.IsNullOrWhiteSpace(bgFormula))
                        continue;

                    // Attempt to parse the RGB values from the formula
                    (int R, int G, int B) parsedColor = ParseRgbFormula(bgFormula);

                    // If parsing failed, treat the color as invalid
                    if (parsedColor == (-1, -1, -1))
                    {
                        Console.Error.WriteLine($"Unable to parse background color for Shape ID {shape.ID} on Page \"{page.Name}\". Formula: {bgFormula}");
                        continue;
                    }

                    // Check whether the parsed color exists in the allowed palette
                    bool isAllowed = allowedPalette.Any(c => c.R == parsedColor.R && c.G == parsedColor.G && c.B == parsedColor.B);

                    // Report any shape whose background color is not part of the corporate palette
                    if (!isAllowed)
                    {
                        Console.WriteLine($"[Violation] Shape ID {shape.ID} on Page \"{page.Name}\" uses disallowed background color {bgFormula}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses a string of the form "RGB(r,g,b)" and returns the integer components.
    /// Returns (-1,-1,-1) if the format is invalid.
    /// </summary>
    private static (int R, int G, int B) ParseRgbFormula(string formula)
    {
        // Expected format: RGB(95,108,53)
        if (!formula.StartsWith("RGB(", StringComparison.OrdinalIgnoreCase) || !formula.EndsWith(")"))
            return (-1, -1, -1);

        // Extract the comma‑separated values inside the parentheses
        string inner = formula.Substring(4, formula.Length - 5);
        string[] parts = inner.Split(',');

        // Ensure exactly three components are present
        if (parts.Length != 3)
            return (-1, -1, -1);

        // Try parsing each component as an integer
        bool okR = int.TryParse(parts[0].Trim(), out int r);
        bool okG = int.TryParse(parts[1].Trim(), out int g);
        bool okB = int.TryParse(parts[2].Trim(), out int b);

        // Return a sentinel tuple if any component fails to parse
        if (!okR || !okG || !okB)
            return (-1, -1, -1);

        return (r, g, b);
    }
}