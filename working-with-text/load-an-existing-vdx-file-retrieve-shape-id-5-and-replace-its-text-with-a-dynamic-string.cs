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
            Console.Error.WriteLine("Usage: Program <input VDX path> [new text]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine the replacement text (dynamic string).
        string newText = args.Length > 1 ? args[1] : $"Updated at {DateTime.Now}";

        // Prepare output path (same directory, different file name).
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "updated.vdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page (index 0).
                Page page = diagram.Pages[0];

                // Shape ID to modify.
                long shapeId = 5L;

                // Retrieve the shape by its ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Replace the shape's text with the new dynamic string.
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt(newText));

                // Save the modified diagram to the output file in VDX format.
                diagram.Save(outputPath, SaveFileFormat.Vdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}