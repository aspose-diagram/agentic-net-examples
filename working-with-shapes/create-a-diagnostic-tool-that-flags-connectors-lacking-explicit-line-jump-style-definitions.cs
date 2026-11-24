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
            Console.Error.WriteLine($"Error: File not found – \"{inputPath}\"");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find connectors.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify 1‑D shapes (connectors).
                    if (shape.OneD)
                    {
                        // Access the line jump style via the layout object.
                        // If the value is PageDefault or Undefined, the connector lacks an explicit jump style.
                        var jumpStyle = shape.Layout?.ConLineJumpStyle?.Value;

                        if (jumpStyle == ConLineJumpStyleValue.PageDefault ||
                            jumpStyle == ConLineJumpStyleValue.Undefined)
                        {
                            Console.WriteLine(
                                $"Connector ID {shape.ID} on Page {page.ID} lacks explicit line jump style (Current value: {jumpStyle}).");
                        }
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