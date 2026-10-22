using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument)
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_highlighted.vsdx");

        // Load the diagram inside a using block (required pattern)
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the plain text of the shape (concatenated Txt runs)
                        string shapeText = shape.Text.Value.ToString() ?? string.Empty;

                        // Flag indicating whether the word "Important" appears in any paragraph
                        bool containsImportant = false;

                        // Paragraph collection exists, but paragraph objects do not expose text directly.
                        // We therefore examine the shape's overall text for the keyword.
                        if (shapeText.IndexOf("Important", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            containsImportant = true;
                        }

                        // If the keyword is found, apply a highlight to the shape
                        if (containsImportant)
                        {
                            // Set the fill foreground color to yellow (#FFFF00)
                            if (shape.Fill != null && shape.Fill.FillForegnd != null)
                            {
                                shape.Fill.FillForegnd.Value = "#FFFF00";
                            }

                            // Set the line (border) color to red (#FF0000) for emphasis
                            if (shape.Line != null && shape.Line.LineColor != null)
                            {
                                shape.Line.LineColor.Value = "#FF0000";
                            }
                        }
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processed and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}