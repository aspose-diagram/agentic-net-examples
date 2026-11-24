using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input folder, output folder, deprecated font, replacement font
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder> <deprecatedFontName> <replacementFontName>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];
        string deprecatedFont = args[2];
        string replacementFont = args[3];

        // Guard: input folder must exist
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists (create if necessary)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        // Process each VSD file in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.vsd");
        foreach (string filePath in files)
        {
            // Guard: file must exist (redundant but required by rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the page
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Iterate through all character runs in the shape
                        for (int charIndex = 0; charIndex < shape.Chars.Count; charIndex++)
                        {
                            // Use fully qualified Aspose.Diagram.Char to avoid ambiguity with System.Char
                            Aspose.Diagram.Char ch = shape.Chars[charIndex];

                            // Replace the deprecated font with the modern equivalent
                            if (ch.FontName.Value == deprecatedFont)
                            {
                                ch.FontName.Value = replacementFont;
                            }
                        }
                    }
                }

                // Prepare output file path (same name with suffix "_updated.vsdx")
                string outputFileName = Path.GetFileNameWithoutExtension(filePath) + "_updated.vsdx";
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}