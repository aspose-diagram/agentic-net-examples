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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine shape type and set conditional EventAfterUpdate logic.
                    // The closest available event cell for “after update” is EventXFMod.
                    // Assign a Visio formula that calls a different macro based on shape type.

                    if (shape.OneD) // Connector shape.
                    {
                        // For connectors, call a connector‑specific macro.
                        shape.Event.EventXFMod.Ufe.F = "ConnectorAction()";
                    }
                    else if (shape.Master != null &&
                             shape.Master.Name != null &&
                             shape.Master.Name.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        // For rectangle master shapes, call a rectangle‑specific macro.
                        shape.Event.EventXFMod.Ufe.F = "RectangleAction()";
                    }
                    else
                    {
                        // Default action for all other shapes.
                        shape.Event.EventXFMod.Ufe.F = "DefaultAction()";
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}