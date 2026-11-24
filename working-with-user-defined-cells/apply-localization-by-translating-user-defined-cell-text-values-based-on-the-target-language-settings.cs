using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file path and target language code.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <targetLanguageCode>");
            return;
        }

        string inputPath = args[0];
        string targetLanguage = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process each user‑defined cell (User) in the shape.
                    foreach (User userCell in shape.Users)
                    {
                        // Original text stored in the Value cell.
                        string originalText = userCell.Value.Val;

                        // Translate the text based on the target language.
                        string translatedText = Translate(originalText, targetLanguage);

                        // Assign the translated text back to the cell.
                        userCell.Value.Val = translatedText;
                    }
                }
            }

            // Build output file path by inserting the language code before the extension.
            string directory = Path.GetDirectoryName(inputPath);
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_{targetLanguage}.vsdx");

            // Save the localized diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Localization completed. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Simple translation stub. In a real scenario this would call a translation service or lookup table.
    /// Here we just append the target language code to demonstrate the concept.
    /// </summary>
    /// <param name="text">The original text to translate.</param>
    /// <param name="languageCode">The target language code (e.g., "fr", "de").</param>
    /// <returns>Translated text.</returns>
    private static string Translate(string text, string languageCode)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        // Example placeholder translation logic.
        return $"{text} ({languageCode})";
    }
}