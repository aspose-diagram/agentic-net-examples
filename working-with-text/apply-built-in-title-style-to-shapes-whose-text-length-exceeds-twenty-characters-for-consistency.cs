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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }
        else
        {
            // Append suffix if no output path provided
            string dir = Path.GetDirectoryName(inputPath);
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{nameWithoutExt}_TitleStyled.vsdx");
        }

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
                Console.Error.WriteLine("Error: \"Title\" style sheet not found in the diagram.");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text of the shape
                    string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                    // Apply the Title style if text length exceeds 20 characters
                    if (shapeText.Length > 20)
                    {
                        shape.TextStyle = titleStyle;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}