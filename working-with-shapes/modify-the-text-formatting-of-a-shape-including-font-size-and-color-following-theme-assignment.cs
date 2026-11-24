using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Apply a preset theme to the page (e.g., Bubble theme)
            page.PresetTheme = PresetThemeValue.Bubble;

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes.");
                return;
            }

            // Get the first shape on the page
            Shape shape = page.Shapes[0];

            // If the shape has no text, add sample text
            if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
            {
                shape.Text.Value.Add(new Txt("Sample Text"));
            }

            // Modify text formatting for each character in the shape
            for (int i = 0; i < shape.Chars.Count; i++)
            {
                // Set font color to red
                shape.Chars[i].Color.Value = "#FF0000";

                // Set font size to 14 points (converted to inches)
                shape.Chars[i].Size.Value = 14.0 / 72.0;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}