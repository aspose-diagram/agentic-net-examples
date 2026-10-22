using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes.");
                return;
            }

            // Get the first shape on the page
            Shape shape = page.Shapes[0];

            // ------------------------------------------------------------
            // Add sample text to the shape if it does not already contain text
            // ------------------------------------------------------------
            if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
            {
                // Add a simple text run
                shape.Text.Value.Add(new Txt("Sample Text"));
            }

            // ------------------------------------------------------------
            // Modify text formatting: set font size and color for the first character run
            // ------------------------------------------------------------
            if (shape.Chars.Count > 0)
            {
                // Font size in points (e.g., 12pt) converted to inches (1 point = 1/72 inch)
                double fontSizeInPoints = 12.0;
                shape.Chars[0].Size.Value = fontSizeInPoints / 72.0;

                // Set font color using a hex string (e.g., red)
                shape.Chars[0].Color.Value = "#FF0000";
            }
            else
            {
                Console.Error.WriteLine("The shape does not contain any character runs to format.");
            }

            // ------------------------------------------------------------
            // Apply a preset theme style to the shape
            // ------------------------------------------------------------
            // TODO: Use the appropriate method (e.g., SetPresetThemeStyleMatrics) to apply a theme style to the shape.
            // Example (method signature may vary):
            // shape.SetPresetThemeStyleMatrics(PresetThemeStyleValue.SomeStyle);
            // Ensure to reference the correct enum/value based on your requirements.

            // ------------------------------------------------------------
            // Save the modified diagram to the output path in VSDX format
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}