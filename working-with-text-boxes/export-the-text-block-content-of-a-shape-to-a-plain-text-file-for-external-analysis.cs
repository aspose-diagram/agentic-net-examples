using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input diagram path and output directory.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDirectory> [shapeId]");
            return;
        }

        string diagramPath = args[0];
        string outputDir = args[1];
        long? targetShapeId = null;

        // Optional third argument specifies a single shape ID to export.
        if (args.Length >= 3 && long.TryParse(args[2], out long parsedId))
        {
            targetShapeId = parsedId;
        }

        // Guard: ensure the input diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Guard: ensure the output directory exists or create it.
        try
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory '{outputDir}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // If a specific shape ID was provided, attempt to locate it on the current page.
                if (targetShapeId.HasValue)
                {
                    Shape shape = page.Shapes.GetShape(targetShapeId.Value);
                    if (shape != null)
                    {
                        ExportShapeText(shape, outputDir);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Shape with ID {targetShapeId.Value} not found on page {page.Name}.");
                    }

                    // Since a specific shape was requested, we can stop after processing the first match.
                    break;
                }
                else
                {
                    // Export text for every shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        ExportShapeText(shape, outputDir);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Extracts the plain‑text content of a shape's text block and writes it to a .txt file.
    /// </summary>
    /// <param name="shape">The shape whose text is to be exported.</param>
    /// <param name="outputDir">The directory where the text file will be saved.</param>
    private static void ExportShapeText(Shape shape, string outputDir)
    {
        try
        {
            // Retrieve the concatenated plain text of the shape.
            string textContent = shape.Text.Value.ToString();

            // Use the shape's ID to create a unique filename.
            string fileName = $"shape_{shape.ID}.txt";
            string outputPath = Path.Combine(outputDir, fileName);

            // Write the text content to the file.
            File.WriteAllText(outputPath, textContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to export text for shape ID {shape.ID}: {ex.Message}");
        }
    }
}