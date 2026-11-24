using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputImagePath> <targetFontName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetFontName = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate over all shapes on the current page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Ensure the shape contains text.
                        if (shape.Text == null || string.IsNullOrEmpty(shape.Text.Value.Text))
                            continue;

                        bool shapeHasTargetFont = false;

                        // Iterate over each character run in the shape to find the target font.
                        for (int charIndex = 0; charIndex < shape.Chars.Count; charIndex++)
                        {
                            // Each character run references a font by index.
                            double fontIdxDouble = shape.Chars[charIndex].Font.Value;
                            int fontIdx = (int)fontIdxDouble;

                            // Guard against out‑of‑range font index.
                            if (fontIdx < 0 || fontIdx >= diagram.Fonts.Count)
                                continue;

                            // Retrieve the font object from the diagram's font collection.
                            Font font = diagram.Fonts[fontIdx];

                            // Compare font name with the target (case‑insensitive).
                            if (string.Equals(font.Name, targetFontName, StringComparison.OrdinalIgnoreCase))
                            {
                                shapeHasTargetFont = true;
                                break; // No need to check further characters in this shape.
                            }
                        }

                        // If the shape contains text using the target font, apply a shadow effect.
                        if (shapeHasTargetFont)
                        {
                            // Configure a simple drop shadow.
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Shadow type.
                            shape.Fill.ShdwForegnd.Value = "#000000";                     // Shadow color (black).
                            shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30% transparency.
                            shape.Fill.ShapeShdwOffsetX.Value = 0.05;                    // Horizontal offset.
                            shape.Fill.ShapeShdwOffsetY.Value = 0.05;                    // Vertical offset.
                        }
                    }
                }

                // Save the modified diagram as an image (PNG) with appropriate save options.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Diagram saved with shadowed text to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}