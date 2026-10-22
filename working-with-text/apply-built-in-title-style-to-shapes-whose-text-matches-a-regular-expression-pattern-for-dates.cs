using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the built‑in "Title" style sheet.
                StyleSheet titleStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Title")
                    {
                        titleStyle = ss;
                        break;
                    }
                }

                if (titleStyle == null)
                {
                    Console.Error.WriteLine("The 'Title' style sheet was not found in the diagram.");
                }

                // Regular expression to match dates (e.g., 12/31/2023).
                Regex dateRegex = new Regex(@"\b\d{2}/\d{2}/\d{4}\b", RegexOptions.Compiled);

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text from the shape.
                        string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                        // If the text matches the date pattern, apply the Title style.
                        if (!string.IsNullOrWhiteSpace(shapeText) && dateRegex.IsMatch(shapeText))
                        {
                            if (titleStyle != null)
                            {
                                // Apply the style to text, fill, and line aspects of the shape.
                                shape.TextStyle = titleStyle;
                                shape.FillStyle = titleStyle;
                                shape.LineStyle = titleStyle;
                            }
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}