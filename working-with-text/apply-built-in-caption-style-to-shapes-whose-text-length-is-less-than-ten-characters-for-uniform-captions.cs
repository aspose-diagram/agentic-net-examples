using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output_CaptionStyle.vsdx");

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
                // Locate the built‑in "Caption" style sheet.
                StyleSheet captionStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Caption")
                    {
                        captionStyle = ss;
                        break;
                    }
                }

                if (captionStyle == null)
                {
                    Console.Error.WriteLine("The built‑in style \"Caption\" was not found in the diagram.");
                    return;
                }

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text of the shape.
                        string text = shape.Text?.Value?.ToString() ?? string.Empty;

                        // Apply the Caption style if text length is less than 10 characters.
                        if (text.Length > 0 && text.Length < 10)
                        {
                            // Assign the Caption style to text, fill, and line style properties.
                            shape.TextStyle = captionStyle;
                            shape.FillStyle = captionStyle;
                            shape.LineStyle = captionStyle;
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}