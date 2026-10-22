using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: check if the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (same folder with suffix "_styled.vsdx")
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_styled.vsdx");

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the built‑in "Title" style sheet
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
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Build plain text from the shape's Txt objects
                    string shapeText = string.Empty;
                    foreach (object item in shape.Text.Value)
                    {
                        if (item is Txt txt)
                        {
                            shapeText += txt.Text;
                        }
                    }

                    // Apply the Title style if text length exceeds 20 characters
                    if (shapeText.Length > 20)
                    {
                        shape.TextStyle = titleStyle;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}