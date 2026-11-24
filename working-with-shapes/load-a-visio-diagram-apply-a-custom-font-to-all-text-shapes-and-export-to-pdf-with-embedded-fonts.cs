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
        // Validate command‑line arguments: input file, output PDF, custom font name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputPdfPath> <customFontName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string customFontName = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Verify that the requested custom font is installed on the system
        bool fontInstalled = false;
        try
        {
            // Enumerate installed fonts using Aspose.Drawing.Text
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            fontInstalled = fontCollection.Families.Any(f => string.Equals(f.Name, customFontName, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Warning: Unable to enumerate installed fonts. Proceeding without validation. Details: {ex.Message}");
        }

        if (!fontInstalled)
        {
            Console.Error.WriteLine($"Warning: Font \"{customFontName}\" is not installed on this system. The PDF may use a fallback font.");
        }

        // Set the default font for rendering fallback scenarios
        FontConfigs.DefaultFontName = customFontName;

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages and shapes to apply the custom font to text runs
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Ensure the shape contains text
                    if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                    {
                        // Apply the custom font to each character run in the shape
                        for (int charIndex = 0; charIndex < shape.Chars.Count; charIndex++)
                        {
                            // FontName is a Cell; set its Value property
                            shape.Chars[charIndex].FontName.Value = customFontName;
                        }
                    }
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Set the default font to ensure missing fonts fall back to the custom font
                DefaultFont = customFontName,
                // Use high resolution for raster images embedded in the PDF
                HorizontalResolution = 300,
                VerticalResolution = 300,
                // Ensure the options know they are for PDF format
                SaveFormat = SaveFileFormat.Pdf
            };

            // Save the diagram as PDF with the configured options
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}