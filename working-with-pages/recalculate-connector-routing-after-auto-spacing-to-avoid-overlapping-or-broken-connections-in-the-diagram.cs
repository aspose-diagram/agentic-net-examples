using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // ---------- Auto‑spacing ----------
                    // Configure auto‑spacing options (default distances).
                    AutoSpaceOptions autoSpaceOpts = new AutoSpaceOptions
                    {
                        DistanceInHorizontal = 2.0,
                        DistanceInVertical = 2.0
                    };

                    // Apply auto‑spacing to all shapes on the page.
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOpts);

                    // ---------- Re‑layout (routing) ----------
                    // Create layout options to recalculate connector routing.
                    LayoutOptions layoutOpts = new LayoutOptions
                    {
                        LayoutStyle = LayoutStyle.FlowChart,
                        Direction = LayoutDirection.TopToBottom,
                        EnlargePage = false // EnlargePage expects a bool, not BOOL.
                    };

                    // Perform layout which also routes connectors.
                    page.Layout(layoutOpts);

                    // ---------- Ensure connector appearance ----------
                    // Iterate shapes to set explicit routing style for connectors.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only 1‑D connector shapes.
                        if (shape.OneD)
                        {
                            // Set routing style to RightAngle to avoid overlaps.
                            shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                            // Set connector visual type to RightAngle lines.
                            shape.SetConnectorsType(ConnectorsTypeValue.RightAngle);
                        }
                    }
                }

                // Save the updated diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
