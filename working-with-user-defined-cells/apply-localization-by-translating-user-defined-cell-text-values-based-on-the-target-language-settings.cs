using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <targetLanguageCode>");
            return;
        }

        string inputPath = args[0];
        string targetLanguage = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Define a simple translation dictionary for demonstration.
        // In a real scenario, replace this with a proper localization service.
        var translations = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            {
                "es", // Spanish
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Hello", "Hola" },
                    { "World", "Mundo" },
                    { "Name", "Nombre" }
                }
            },
            {
                "fr", // French
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Hello", "Bonjour" },
                    { "World", "Monde" },
                    { "Name", "Nom" }
                }
            }
            // Add more language dictionaries as needed.
        };

        // Load the Visio diagram inside a try/catch block.
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

        // Iterate through all pages and shapes.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Process each user-defined cell.
                foreach (User userCell in shape.Users)
                {
                    // Retrieve the original text.
                    string originalText = userCell.Value.Val ?? string.Empty;

                    // Translate the text based on the target language.
                    string translatedText = Translate(originalText, targetLanguage, translations);

                    // Update the cell only if translation changed the text.
                    if (!string.Equals(originalText, translatedText, StringComparison.Ordinal))
                    {
                        userCell.Value.Val = translatedText;
                    }
                }
            }
        }

        // Prepare output path.
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + $"_{targetLanguage}.vsdx");

        // Save the modified diagram.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Translates a given text to the target language using the provided dictionary.
    /// If no translation is found, the original text is returned.
    /// </summary>
    static string Translate(string text, string targetLang, Dictionary<string, Dictionary<string, string>> dict)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(targetLang))
            return text;

        if (dict.TryGetValue(targetLang, out var langDict))
        {
            if (langDict.TryGetValue(text, out var translated))
                return translated;
        }

        // Fallback: return original text.
        return text;
    }
}