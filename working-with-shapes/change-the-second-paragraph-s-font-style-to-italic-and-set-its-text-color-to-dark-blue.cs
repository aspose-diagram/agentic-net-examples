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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Get the first shape on the page.
            Shape shape = page.Shapes[0];

            // Verify the shape has at least two paragraphs.
            if (shape.Paras.Count < 2)
            {
                Console.Error.WriteLine("The selected shape does not contain a second paragraph.");
                return;
            }

            // Apply italic style and dark blue color to all characters of the shape.
            // (Aspose.Diagram applies formatting at the character level.)
            for (int i = 0; i < shape.Chars.Count; i++)
            {
                // Set italic style.
                shape.Chars[i].Style.Value = StyleValue.Italic;

                // Set text color to dark blue (#00008B).
                shape.Chars[i].Color.Value = "#00008B";
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}