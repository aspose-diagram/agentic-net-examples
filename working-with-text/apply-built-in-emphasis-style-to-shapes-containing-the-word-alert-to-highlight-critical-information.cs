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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the built‑in "Emphasis" style sheet.
                StyleSheet emphasisStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    // StyleSheet.Name is a plain string.
                    if (!string.IsNullOrEmpty(ss.Name) &&
                        ss.Name.Equals("Emphasis", StringComparison.OrdinalIgnoreCase))
                    {
                        emphasisStyle = ss;
                        break;
                    }
                }

                if (emphasisStyle == null)
                {
                    Console.Error.WriteLine("Emphasis style sheet not found in the diagram.");
                    // Continue without applying style.
                }

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    // ShapeCollection implements IEnumerable, iterate directly.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the shape's text content safely.
                        string shapeText = shape.Text?.Value?.ToString() ?? string.Empty;

                        // Check if the text contains the word "Alert" (case‑insensitive).
                        if (shapeText.IndexOf("Alert", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            if (emphasisStyle != null)
                            {
                                // Apply the Emphasis style to text, fill, and line.
                                shape.TextStyle = emphasisStyle;
                                shape.FillStyle = emphasisStyle;
                                shape.LineStyle = emphasisStyle;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}