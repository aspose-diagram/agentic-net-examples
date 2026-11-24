using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <pageIndex> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];
        string outputPath = args.Length >= 3 ? args[2] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_cleaned.vsdx");

        // Guard: input file must exist.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: page index must be a valid integer.
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine($"Invalid page index: {pageIndexArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Guard: ensure the requested page exists.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the target page.
                Page page = diagram.Pages[pageIndex];

                // Iterate over all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // If the shape contains no text runs, consider its paragraphs empty.
                    // shape.Text.Value is the collection of text runs (FormatTxt objects).
                    if (shape.Text != null && shape.Text.Value != null && shape.Text.Value.Count == 0)
                    {
                        // Remove all paragraphs from the shape.
                        // ParaCollection does not support RemoveAt; the pattern is to clear and re‑add.
                        shape.Paras.Clear();
                    }
                }

                // Save the cleaned diagram to the output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}