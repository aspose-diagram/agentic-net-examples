using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, page index, output folder.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <pageIndex> <outputFolder>");
            return;
        }

        // Assign arguments to variables.
        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Parse page index; guard against invalid integer format.
        if (!int.TryParse(args[1], out int pageIndex))
        {
            Console.Error.WriteLine($"Invalid page index: {args[1]}");
            return;
        }

        string outputFolder = args[2];
        // Guard: create the output folder if it does not exist.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Load the Visio diagram inside a try/catch to capture loading errors.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Guard: ensure the requested page index is within range.
        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
        {
            Console.Error.WriteLine($"Page index out of range. Diagram has {diagram.Pages.Count} pages.");
            diagram.Dispose();
            return;
        }

        // Retrieve the specific page.
        Page page = diagram.Pages[pageIndex];

        // Iterate over each shape on the page.
        foreach (Shape shape in page.Shapes)
        {
            // Skip shapes that are marked as deleted.
            if (shape.Del == BOOL.True)
                continue;

            // Build a unique file name for the shape HTML output.
            // Prefer the universal name; fall back to shape ID if name is empty.
            string shapeName = string.IsNullOrWhiteSpace(shape.NameU) ? $"Shape_{shape.ID}" : shape.NameU;
            // Replace any invalid filename characters.
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
                shapeName = shapeName.Replace(invalidChar, '_');

            string outputPath = Path.Combine(outputFolder, $"{shapeName}.html");

            // Export the shape to HTML with embedded CSS.
            try
            {
                // Create HTML save options; default settings embed CSS.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                // Export hidden shapes on the page as false to keep output clean.
                htmlOptions.ExportHiddenPage = false;
                // Perform the export for the current shape.
                shape.ToHTML(outputPath, htmlOptions);
                Console.WriteLine($"Exported shape ID {shape.ID} to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to export shape ID {shape.ID}: {ex.Message}");
            }
        }

        // Dispose the diagram to release resources.
        diagram.Dispose();
    }
}