using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    // Simple translation stub – replace with real translation service as needed
    static string Translate(string text, string targetLanguage)
    {
        // Example: English to Spanish dictionary
        var dictionary = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Hello", "Hola" },
            { "World", "Mundo" },
            { "Sample", "Ejemplo" }
            // Add more entries as required
        };

        if (dictionary.TryGetValue(text, out var translated))
            return translated;

        // If no translation found, return the original text
        return text;
    }

    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            const string inputPath = "input.vsdx";
            // Path for the translated output diagram
            const string outputPath = "output_translated.vsdx";
            // Target language code (e.g., "es" for Spanish)
            const string targetLanguage = "es";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process user-defined cells (custom properties)
                    foreach (User userCell in shape.Users)
                    {
                        // Original cell value
                        string originalValue = userCell.Value.Val;

                        // Translate the value
                        string translatedValue = Translate(originalValue, targetLanguage);

                        // Assign the translated text back to the cell
                        userCell.Value.Val = translatedValue;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
