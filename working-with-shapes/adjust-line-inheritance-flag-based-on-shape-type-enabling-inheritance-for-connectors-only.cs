using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // Determine if the shape is a connector (1‑D shape)
                    bool isConnector = shape.OneD;

                    if (isConnector)
                    {
                        // Enable line inheritance for connectors by setting color to "inherit"
                        shape.Line.LineColor.Value = "inherit";
                        // Keep other line properties unchanged to inherit defaults
                    }
                    else
                    {
                        // For non‑connectors, set explicit line properties (example: solid black line)
                        shape.Line.LineColor.Value = "#000000";
                        shape.Line.LineWeight.Value = 0.02; // inches
                        shape.Line.LinePattern.Value = LinePatternValue.Solid;
                        shape.Line.BeginArrow.Value = 0; // no arrow
                        shape.Line.EndArrow.Value = 0;   // no arrow
                    }
                }
            }

            // Save the modified diagram using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}