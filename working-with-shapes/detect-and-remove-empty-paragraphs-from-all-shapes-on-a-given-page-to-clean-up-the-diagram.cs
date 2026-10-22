using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, page index, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <pageIndex> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];
        string outputPath = args[2];

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: output directory must exist (create if necessary)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Parse page index
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Invalid page index. It must be a non‑negative integer.");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate page index range
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the target page
                Page page = diagram.Pages[pageIndex];

                // Process each shape on the page (including shapes inside groups)
                foreach (Shape shape in page.Shapes)
                {
                    ProcessShape(shape);
                }

                // Save the cleaned diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Recursively processes a shape: removes empty paragraphs and handles group children
    private static void ProcessShape(Shape shape)
    {
        // Skip deleted shapes
        if (shape.Del == BOOL.True)
            return;

        // Remove empty paragraphs from the current shape
        RemoveEmptyParagraphs(shape);

        // If the shape is a group, process its child shapes recursively
        if (shape.Type == TypeValue.Group && shape.Shapes != null)
        {
            foreach (Shape child in shape.Shapes)
            {
                ProcessShape(child);
            }
        }
    }

    // Removes paragraphs that contain no text from a shape
    private static void RemoveEmptyParagraphs(Shape shape)
    {
        // Get the plain text of the shape
        string plainText = shape.Text.Value.ToString();

        // If the shape has no text (empty or whitespace), clear all paragraphs
        if (string.IsNullOrWhiteSpace(plainText))
        {
            // Clear existing paragraphs
            shape.Paras.Clear();
        }
        // Otherwise, keep existing paragraphs (no action needed)
    }
}