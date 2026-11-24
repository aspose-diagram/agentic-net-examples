using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page
                Page page = diagram.Pages[0];

                // Text to display
                string text = "Sample Text";

                // Position and size for the main (white) text shape
                double pinX = 2.0;   // X coordinate (in inches)
                double pinY = 2.0;   // Y coordinate (in inches)
                double width = 2.0;  // Width (in inches)
                double height = 0.5; // Height (in inches)

                // -----------------------------------------------------------------
                // Create a black outline shape slightly larger than the white text.
                // This simulates a 1‑pixel border effect.
                // -----------------------------------------------------------------
                double outlineOffset = 0.05; // small offset to simulate outline thickness
                double outlinePinX = pinX - outlineOffset;
                double outlinePinY = pinY - outlineOffset;
                double outlineWidth = width + outlineOffset * 2;
                double outlineHeight = height + outlineOffset * 2;

                // Add the outline shape (added first so it appears behind the white text)
                Shape outlineShape = page.AddText(outlinePinX, outlinePinY, outlineWidth, outlineHeight, text);

                // Clear any default character runs and add a new one with black color
                outlineShape.Chars.Clear();
                outlineShape.Chars.Add(new Aspose.Diagram.Char());
                outlineShape.Chars[0].IX = 0;
                outlineShape.Chars[0].Color.Value = "#000000"; // Black color

                // Ensure the text content is the desired string
                outlineShape.Text.Value.Clear();
                outlineShape.Text.Value.Add(new Txt(text));

                // -----------------------------------------------------------------
                // Create the white text shape that sits on top of the outline.
                // -----------------------------------------------------------------
                Shape whiteShape = page.AddText(pinX, pinY, width, height, text);

                // Clear any default character runs and add a new one with white color
                whiteShape.Chars.Clear();
                whiteShape.Chars.Add(new Aspose.Diagram.Char());
                whiteShape.Chars[0].IX = 0;
                whiteShape.Chars[0].Color.Value = "#FFFFFF"; // White color

                // Ensure the text content is the desired string
                whiteShape.Text.Value.Clear();
                whiteShape.Text.Value.Add(new Txt(text));

                // -----------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}