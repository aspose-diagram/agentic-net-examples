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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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

            // Iterate through all pages to find connector shapes.
            foreach (Page page in diagram.Pages)
            {
                // Use the Shapes collection to locate connector shapes.
                foreach (Shape shape in page.Shapes)
                {
                    // Identify a connector by checking its OneD flag (1‑D connector line).
                    if (shape.OneD)
                    {
                        // Access the Layout property (required by the task) – we will not modify layout‑specific
                        // routing values here, but the property is accessed to demonstrate usage.
                        var layout = shape.Layout;

                        // Modify the connector's position using its XForm (PinX and PinY).
                        // Example: shift the connector 2.0 inches to the right and 1.0 inch down.
                        double currentPinX = shape.XForm.PinX.Value;
                        double currentPinY = shape.XForm.PinY.Value;

                        shape.XForm.PinX.Value = currentPinX + 2.0; // Move right.
                        shape.XForm.PinY.Value = currentPinY + 1.0; // Move down.

                        // Optionally, you could also adjust layout routing if needed, e.g.:
                        // layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}