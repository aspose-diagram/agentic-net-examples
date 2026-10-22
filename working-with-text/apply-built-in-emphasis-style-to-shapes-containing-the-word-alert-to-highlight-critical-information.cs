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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the built‑in style sheet named "Emphasis".
                StyleSheet emphasisStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name != null && ss.Name.Equals("Emphasis", StringComparison.OrdinalIgnoreCase))
                    {
                        emphasisStyle = ss;
                        break;
                    }
                }

                if (emphasisStyle == null)
                {
                    Console.Error.WriteLine("Error: \"Emphasis\" style sheet not found in the diagram.");
                    return;
                }

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the shape's text content safely.
                        string shapeText = shape.Text?.Value?.ToString() ?? string.Empty;

                        // Check if the text contains the word "Alert".
                        if (shapeText.Contains("Alert", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply the Emphasis style to the shape's text.
                            shape.TextStyle = emphasisStyle;
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}