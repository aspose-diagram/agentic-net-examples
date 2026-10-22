using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram path, source font name, target font name, output diagram path
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <sourceFontName> <targetFontName> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string sourceFontName = args[1];
        string targetFontName = args[2];
        string outputPath = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Find indices of source and target fonts in the diagram's font collection
            int sourceFontIndex = -1;
            int targetFontIndex = -1;

            for (int i = 0; i < diagram.Fonts.Count; i++)
            {
                Font font = diagram.Fonts[i];
                if (font.Name.Equals(sourceFontName, StringComparison.OrdinalIgnoreCase))
                {
                    sourceFontIndex = i;
                }
                if (font.Name.Equals(targetFontName, StringComparison.OrdinalIgnoreCase))
                {
                    targetFontIndex = i;
                }
            }

            // If the source font is not present, nothing to replace
            if (sourceFontIndex == -1)
            {
                Console.WriteLine($"Source font \"{sourceFontName}\" not found in diagram. No replacement performed.");
            }
            else
            {
                // Add target font to the collection if it does not already exist
                if (targetFontIndex == -1)
                {
                    Font newFont = new Font();
                    newFont.Name = targetFontName;
                    diagram.Fonts.Add(newFont);
                    targetFontIndex = diagram.Fonts.Count - 1;
                    Console.WriteLine($"Target font \"{targetFontName}\" added to diagram font collection.");
                }

                // Iterate through all pages, shapes, and characters to replace font indices
                for (int p = 0; p < diagram.Pages.Count; p++)
                {
                    Page page = diagram.Pages[p];
                    for (int s = 0; s < page.Shapes.Count; s++)
                    {
                        Shape shape = page.Shapes[s];

                        // Process only shapes that have character formatting
                        if (shape.Chars != null && shape.Chars.Count > 0)
                        {
                            for (int c = 0; c < shape.Chars.Count; c++)
                            {
                                // Use fully qualified type to avoid ambiguity with System.Char
                                Aspose.Diagram.Char ch = shape.Chars[c];
                                int currentFontIdx = ch.Font.Value;
                                if (currentFontIdx == sourceFontIndex)
                                {
                                    ch.Font.Value = targetFontIndex;
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"All occurrences of font \"{sourceFontName}\" have been replaced with \"{targetFontName}\".");
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}