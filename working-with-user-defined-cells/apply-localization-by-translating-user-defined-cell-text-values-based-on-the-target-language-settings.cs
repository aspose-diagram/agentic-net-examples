using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    // Simple placeholder translation method.
    // In a real scenario, replace this with a call to a translation service.
    private static string Translate(string text, string targetLanguage)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        // Example: append language code to simulate translation.
        return $"{text}_{targetLanguage}";
    }

    public static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files.
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Target language code (e.g., "fr" for French, "es" for Spanish).
            string targetLanguage = "fr";

            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages, shapes, and user-defined cells.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        // Original cell value.
                        string originalValue = userCell.Value.Val;

                        // Translate the value.
                        string translatedValue = Translate(originalValue, targetLanguage);

                        // Update the cell with the translated text.
                        userCell.Value.Val = translatedValue;
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
