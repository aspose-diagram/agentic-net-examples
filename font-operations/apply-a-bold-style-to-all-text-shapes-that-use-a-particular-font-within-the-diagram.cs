using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (required)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output diagram path (optional, defaults to output.vsdx)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";
        // Font name to search for (optional, defaults to Arial)
        string targetFont = args.Length > 2 ? args[2] : "Arial";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape contains text (non‑null and not empty)
                        if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            continue;

                        // Iterate over each character run in the shape
                        for (int i = 0; i < shape.Chars.Count; i++)
                        {
                            Aspose.Diagram.Char ch = shape.Chars[i];

                            // Check if the character's font name matches the target font
                            if (ch.FontName != null && ch.FontName.Value == targetFont)
                            {
                                // Apply bold style while preserving existing styles
                                ch.Style.Value |= StyleValue.Bold;
                            }
                        }
                    }
                }

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}