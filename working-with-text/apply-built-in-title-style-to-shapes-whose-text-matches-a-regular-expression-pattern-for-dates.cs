using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output file path (second argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Regular expression to detect dates (e.g., 2023-08-27 or 2023/08/27)
        Regex dateRegex = new Regex(@"\b\d{4}[-/]\d{2}[-/]\d{2}\b", RegexOptions.Compiled);

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Locate the built‑in "Title" style sheet (if it exists)
            StyleSheet? titleStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Title")
                {
                    titleStyle = ss;
                    break;
                }
            }

            // If the "Title" style is not present, report and exit
            if (titleStyle == null)
            {
                Console.Error.WriteLine("The built‑in 'Title' style was not found in the document.");
                return;
            }

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text of the shape; empty text is ignored
                    string shapeText = shape.Text.Value.Text ?? string.Empty;

                    // Apply the style only if the text matches the date pattern
                    if (dateRegex.IsMatch(shapeText))
                    {
                        // Assign the Title style to text, fill, and line formatting
                        shape.TextStyle = titleStyle;
                        shape.FillStyle = titleStyle;
                        shape.LineStyle = titleStyle;
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}