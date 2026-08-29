using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Simple mock translation method.
        // In a real scenario, replace this with a call to a translation service.
        static string Translate(string text, string targetLanguage)
        {
            // Example: append language code to demonstrate translation.
            return $"{text} [{targetLanguage}]";
        }

        static void Main()
        {
            try
            {

                // Path to the source Visio file.
                string inputPath = "input.vsdx";

                // Path for the localized output file.
                string outputPath = "output_localized.vsdx";

                // Target language code (e.g., "es" for Spanish, "fr" for French).
                string targetLanguage = "es";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through user-defined cells (Users collection).
                        foreach (User userCell in shape.Users)
                        {
                            // Original value.
                            string originalValue = userCell.Value.Val;

                            // Translate the value.
                            string translatedValue = Translate(originalValue, targetLanguage);

                            // Assign the translated text back to the cell.
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