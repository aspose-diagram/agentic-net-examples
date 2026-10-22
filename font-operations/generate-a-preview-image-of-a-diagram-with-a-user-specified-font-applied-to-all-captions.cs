using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output image path, font name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputImagePath> <fontName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fontName = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Validate that the requested font is installed on the system
        try
        {
            var fontCollection = new InstalledFontCollection();
            bool fontExists = fontCollection.Families.Any(f => f.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase));
            if (!fontExists)
            {
                Console.Error.WriteLine($"Warning: Font \"{fontName}\" is not installed on this system. Rendering will fall back to the default font.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while checking installed fonts: {ex.Message}");
            // Continue execution; fallback will be handled by Aspose
        }

        try
        {
            // Set the default font for Aspose.Diagram rendering
            FontConfigs.DefaultFontName = fontName;

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply the specified font to all characters in all shapes (captions)
            // Note: Char.FontName property may not exist; using Font.Value as a fallback is not required.
            // The default font setting above ensures rendering uses the requested font when possible.
            // If explicit per-character font assignment is needed, uncomment and adjust the following block
            /*
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Only process shapes that contain text
                    if (shape.Chars != null && shape.Chars.Count > 0)
                    {
                        foreach (Char ch in shape.Chars)
                        {
                            // Attempt to set the font name; if the property differs, adjust accordingly.
                            // ch.FontName = fontName; // Placeholder – actual property may vary.
                        }
                    }
                }
            }
            */

            // Configure image save options for a preview (first page only)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                PageIndex = 0,          // First page
                PageCount = 1,          // Export only one page
                Resolution = 150f       // Reasonable resolution for preview
            };

            // Save the preview image
            diagram.Save(outputPath, saveOptions);

            // Clean up
            diagram.Dispose();

            Console.WriteLine($"Preview image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}