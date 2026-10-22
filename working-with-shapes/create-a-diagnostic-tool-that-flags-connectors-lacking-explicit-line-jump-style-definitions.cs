using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: DiagnosticTool <input‑visio‑file>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the page.
                for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                {
                    Shape shape = page.Shapes[shapeIdx];

                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify connector shapes (1‑D shapes).
                    if (!shape.OneD)
                        continue;

                    // Ensure the layout cell collection is available.
                    if (shape.Layout == null)
                        continue;

                    // Retrieve the line jump style value.
                    var jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                    // Flag connectors that use the page default or undefined style
                    // (i.e., no explicit jump style has been set).
                    if (jumpStyle == ConLineJumpStyleValue.PageDefault ||
                        jumpStyle == ConLineJumpStyleValue.Undefined)
                    {
                        Console.WriteLine($"Connector flagged: Page {pageIndex + 1}, Shape ID {shape.ID}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}