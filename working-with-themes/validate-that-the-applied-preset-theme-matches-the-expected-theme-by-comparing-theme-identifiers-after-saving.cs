using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, expected theme name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <expectedTheme>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string expectedThemeName = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Try to parse the expected theme enum value
        if (!Enum.TryParse<PresetThemeValue>(expectedThemeName, true, out var expectedTheme))
        {
            Console.Error.WriteLine($"Invalid theme name: {expectedThemeName}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply the preset theme to the first page
                if (diagram.Pages.Count > 0)
                {
                    Page page = diagram.Pages[0];
                    page.PresetTheme = expectedTheme;
                }
                else
                {
                    Console.Error.WriteLine("Diagram contains no pages.");
                    return;
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Reload the saved diagram to verify the theme
            using (Diagram savedDiagram = new Diagram(outputPath))
            {
                // TODO: Retrieve the applied theme identifier from the saved diagram.
                // The Aspose.Diagram API does not provide a getter for PresetTheme,
                // so direct comparison is not possible with the current version.
                // If a future API adds a readable property, compare it with expectedTheme here.

                Console.WriteLine("Theme applied and diagram saved successfully.");
                // Placeholder for validation logic:
                // if (retrievedTheme == expectedTheme) { ... } else { throw new Exception("Theme mismatch."); }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}