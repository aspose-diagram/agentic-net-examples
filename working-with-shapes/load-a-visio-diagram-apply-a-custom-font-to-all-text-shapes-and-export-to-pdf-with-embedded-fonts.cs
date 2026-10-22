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
        // Validate arguments: input file, output file, optional custom font name.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath> [customFontName]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string customFontName = args.Length >= 3 ? args[2] : "Arial";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Set the default fallback font for rendering.
            FontConfigs.DefaultFontName = customFontName;

            // OPTIONAL: Log whether the custom font is installed on the system.
            // This does not block execution if the font is missing.
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            bool fontInstalled = installedFonts.Families.Any(f => string.Equals(f.Name, customFontName, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"Custom font \"{customFontName}\" installed on system: {fontInstalled}");

            // Iterate through all pages and shapes to apply the custom font to text shapes.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Check if the shape contains text.
                    if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                    {
                        // Apply the custom font to each character run in the shape.
                        for (int charIndex = 0; charIndex < shape.Chars.Count; charIndex++)
                        {
                            // Set the FontName cell value.
                            shape.Chars[charIndex].FontName.Value = customFontName;
                        }
                    }
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = customFontName; // Fallback font if required.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set the format tracker.

            // Save the diagram as PDF. Note: Aspose.Diagram does not support explicit font embedding.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved to PDF successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}