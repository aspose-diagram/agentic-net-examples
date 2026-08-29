using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Desired custom font name (third argument or default)
        string customFontName = args.Length > 2 ? args[2] : "Arial";

        // Configure system font folder for Aspose.Diagram
        try
        {
            // Retrieve the OS fonts directory
            string systemFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            // Register the font folder (recursive search)
            FontConfigs.SetFontFolder(systemFontFolder, true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error configuring font folder: {ex.Message}");
            return;
        }

        // Verify that the requested custom font is installed on the system
        try
        {
            var fontCollection = new InstalledFontCollection();
            bool fontFound = false;
            // Iterate over installed font families (use var as per guidelines)
            foreach (var family in fontCollection.Families)
            {
                // Compare font names case‑insensitively
                if (string.Equals(family.Name, customFontName, StringComparison.OrdinalIgnoreCase))
                {
                    fontFound = true;
                    break;
                }
            }

            if (!fontFound)
            {
                Console.Error.WriteLine($"Warning: Font '{customFontName}' not found in installed fonts.");
            }

            // Set the default fallback font for Aspose.Diagram
            FontConfigs.DefaultFontName = customFontName;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during font validation: {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Apply the custom font to every shape that contains text
        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape actually has visible text
                    if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                    {
                        // Ensure at least one character formatting entry exists
                        if (shape.Chars.Count == 0)
                        {
                            // Create a Char covering the whole text (IX = 0)
                            Aspose.Diagram.Char firstChar = new Aspose.Diagram.Char();
                            firstChar.IX = 0;
                            shape.Chars.Add(firstChar);
                        }

                        // Apply the custom font to each Char in the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            ch.FontName.Value = customFontName;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error applying font to shapes: {ex.Message}");
            return;
        }

        // Prepare PDF save options with the custom font as default
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = customFontName; // Fallback font if any text lacks explicit font

        // Save the diagram as PDF (fonts are embedded automatically when available)
        try
        {
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving PDF: {ex.Message}");
        }
    }
}