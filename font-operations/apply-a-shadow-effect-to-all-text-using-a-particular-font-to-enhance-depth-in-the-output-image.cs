using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputImagePath> <targetFontName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetFontName = args[2];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the index of the target font in the diagram's font collection
            int targetFontIndex = -1;
            for (int i = 0; i < diagram.Fonts.Count; i++)
            {
                Font font = diagram.Fonts[i];
                if (string.Equals(font.Name, targetFontName, StringComparison.OrdinalIgnoreCase))
                {
                    targetFontIndex = i;
                    break;
                }
            }

            if (targetFontIndex == -1)
            {
                Console.Error.WriteLine($"Font \"{targetFontName}\" not found in diagram fonts. No shadow will be applied.");
            }

            // Iterate through all pages
            for (int p = 0; p < diagram.Pages.Count; p++)
            {
                Page page = diagram.Pages[p];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without text
                    if (shape.Text == null || string.IsNullOrEmpty(shape.Text.Value.Text))
                        continue;

                    bool applyShadow = false;

                    // If the target font was found, check each character's font index
                    if (targetFontIndex != -1 && shape.Chars != null && shape.Chars.Count > 0)
                    {
                        for (int c = 0; c < shape.Chars.Count; c++)
                        {
                            // Font.Value holds the index into diagram.Fonts
                            if (shape.Chars[c].Font != null && shape.Chars[c].Font.Value == targetFontIndex)
                            {
                                applyShadow = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        // If target font not found, apply shadow to all text shapes
                        applyShadow = true;
                    }

                    if (applyShadow)
                    {
                        // Apply a simple drop shadow to the shape's fill
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;          // Shadow type
                        shape.Fill.ShdwForegnd.Value = "#000000";                           // Shadow color (black)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;                            // 30% transparency
                        shape.Fill.ShapeShdwOffsetX.Value = 0.05;                           // Horizontal offset
                        shape.Fill.ShapeShdwOffsetY.Value = 0.05;                           // Vertical offset
                    }
                }
            }

            // Save the modified diagram as an image (PNG)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}