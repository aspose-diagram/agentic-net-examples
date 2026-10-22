using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, optional character limit.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [maxParagraphLength]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int maxLength = 200; // Default character limit per paragraph.

        if (args.Length >= 3 && !int.TryParse(args[2], out maxLength))
        {
            Console.Error.WriteLine("Invalid maxParagraphLength argument. It must be an integer.");
            return;
        }

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether validation passed.
            bool validationPassed = true;

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build the full text of the shape by concatenating Txt objects.
                    string shapeText = string.Empty;
                    foreach (object item in shape.Text.Value)
                    {
                        if (item is Txt txt)
                        {
                            shapeText += txt.Text;
                        }
                    }

                    // Split the text into paragraphs using line‑feed as the delimiter.
                    string[] paragraphs = shapeText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    // Validate each paragraph's length.
                    for (int p = 0; p < paragraphs.Length; p++)
                    {
                        string paragraph = paragraphs[p];
                        if (paragraph.Length > maxLength)
                        {
                            validationPassed = false;
                            Console.Error.WriteLine($"Paragraph length exceeds limit (>{maxLength}) in shape ID {shape.ID} on page '{page.Name}', paragraph {p + 1} (length {paragraph.Length}).");
                        }
                    }
                }
            }

            if (!validationPassed)
            {
                Console.Error.WriteLine("Validation failed. Diagram will not be saved.");
                return;
            }

            // Save the diagram after successful validation.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}