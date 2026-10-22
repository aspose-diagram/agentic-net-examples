using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define new dimensions for non‑connector shapes (in inches).
                double newWidth = 2.0;
                double newHeight = 1.0;

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Resize all shapes that are not connectors.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Non‑connector shape (OneD == false).
                        if (!shape.OneD)
                        {
                            // Set width and height via the XForm section.
                            shape.XForm.Width.Value = newWidth;
                            shape.XForm.Height.Value = newHeight;
                        }
                    }

                    // Recalculate routing for connector shapes.
                    foreach (Shape connector in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (connector.Del == BOOL.True)
                            continue;

                        // Connector shape (OneD == true).
                        if (connector.OneD)
                        {
                            // Set routing style to right‑angle.
                            connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                            // Ensure the connector appearance matches the routing style.
                            connector.SetConnectorsType(ConnectorsTypeValue.RightAngle);
                            // Force reroute if needed.
                            connector.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}