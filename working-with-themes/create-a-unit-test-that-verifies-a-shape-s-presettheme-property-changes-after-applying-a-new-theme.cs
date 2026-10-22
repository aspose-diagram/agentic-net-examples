using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "Themes1.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define an output path for the modified diagram
        string outputPath = "Themes1_Modified.vsdx";

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                Page firstPage = diagram.Pages[0];
                if (firstPage.Shapes.Count == 0)
                {
                    throw new Exception("The first page contains no shapes.");
                }

                // Retrieve the first shape on the first page
                Shape shape = firstPage.Shapes[0];

                // Apply a new preset theme to the shape (write‑only property)
                shape.PresetTheme = PresetThemeValue.Bubble;

                // TODO: Verify that the theme was applied.
                // The PresetTheme property is write‑only and cannot be read back.
                // Verification could be performed by inspecting visual changes after saving
                // or by comparing expected style attributes, which is beyond the scope here.

                // Save the modified diagram to verify that no exceptions occur during save
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}