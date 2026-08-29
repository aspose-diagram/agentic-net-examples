using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation; // Required for ConnectionPointPlace if needed

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output Visio file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

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
                    // Identify connector shapes: they are 1‑D shapes (OneD == true)
                    if (shape.OneD)
                    {
                        // Set the connector routing style to straight lines (optional)
                        shape.SetConnectorsType(ConnectorsTypeValue.StraightLines);

                        // Apply a dashed line pattern to create the visual “dashed” style
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;

                        // Increase line weight slightly to make the dash more visible
                        shape.Line.LineWeight.Value = 0.03; // inches

                        // Set a bright line color (e.g., light cyan) to simulate a glow effect
                        shape.Line.LineColor.Value = "#00FFFF";

                        // Note: Aspose.Diagram does not expose a direct Glow or Shadow property.
                        // The bright line color combined with increased weight and dash pattern
                        // provides a simple glow-like appearance for connectors.
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format
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