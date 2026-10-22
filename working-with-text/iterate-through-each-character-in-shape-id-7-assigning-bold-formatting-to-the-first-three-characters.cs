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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with ID 7 across all pages.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape returns null if the ID is not present on this page.
                targetShape = page.Shapes.GetShape(7);
                if (targetShape != null)
                {
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("Error: Shape with ID 7 was not found in the diagram.");
                return;
            }

            // Ensure the shape has at least three characters.
            int charsToFormat = Math.Min(3, targetShape.Chars.Count);

            // Apply bold formatting to the first three characters.
            for (int i = 0; i < charsToFormat; i++)
            {
                // Set the style to Bold. Existing style bits are overwritten as per requirement.
                targetShape.Chars[i].Style.Value = StyleValue.Bold;
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}