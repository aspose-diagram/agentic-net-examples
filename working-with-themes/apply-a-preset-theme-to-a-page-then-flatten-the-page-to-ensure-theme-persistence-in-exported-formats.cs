using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "Themes1_Themed.vsdx";

        // Guard: ensure the source diagram exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0)
            Page page = diagram.Pages[0];

            // Apply a preset theme to the page.
            // The Bubble theme is used as an example; change as needed.
            page.PresetTheme = PresetThemeValue.Bubble;

            // Note: Diagram/Page flattening is not required.
            // Theme information is persisted automatically when saving to VSDX.

            // Save the modified diagram to a new file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Theme applied and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}