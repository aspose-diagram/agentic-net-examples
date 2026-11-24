using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the built‑in "Subtitle" style sheet.
            StyleSheet subtitleStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Subtitle")
                {
                    subtitleStyle = ss;
                    break;
                }
            }

            if (subtitleStyle == null)
            {
                Console.Error.WriteLine("Error: \"Subtitle\" style sheet not found in the diagram.");
                return;
            }

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text of the shape.
                    string shapeText = shape.Text?.Value?.ToString() ?? string.Empty;

                    // Check if the text starts with a numeric prefix.
                    if (!string.IsNullOrWhiteSpace(shapeText) && char.IsDigit(shapeText.TrimStart()[0]))
                    {
                        // Apply the "Subtitle" style to the shape's text.
                        shape.TextStyle = subtitleStyle;
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}