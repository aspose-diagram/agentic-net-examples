using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the built‑in 'Subtitle' style sheet
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
                Console.Error.WriteLine("Warning: 'Subtitle' style sheet not found in the diagram. No styles will be applied.");
            }
            else
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text of the shape
                        string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                        // Trim leading whitespace for accurate prefix check
                        string trimmedText = shapeText.TrimStart();

                        // Check if the text starts with a numeric character
                        if (!string.IsNullOrEmpty(trimmedText) && char.IsDigit(trimmedText[0]))
                        {
                            // Apply the 'Subtitle' style to the shape's text
                            shape.TextStyle = subtitleStyle;
                        }
                    }
                }
            }

            // Prepare output path (same directory with a suffix)
            string outputDir = Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
            string outputPath = Path.Combine(outputDir, "output_subtitle.vsdx");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}