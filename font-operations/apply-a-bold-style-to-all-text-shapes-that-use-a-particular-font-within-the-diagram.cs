using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input diagram, target font name, output diagram
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <targetFontName> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string targetFontName = args[1];
        string outputPath = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape contains a Text object
                    if (shape.Text == null)
                        continue;

                    // Iterate over each character formatting entry in the shape
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Compare the character's font name with the target font (case‑insensitive)
                        if (string.Equals(ch.FontName.Value, targetFontName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply bold style while preserving existing styles
                            ch.Style.Value |= StyleValue.Bold;
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Bold style applied successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}