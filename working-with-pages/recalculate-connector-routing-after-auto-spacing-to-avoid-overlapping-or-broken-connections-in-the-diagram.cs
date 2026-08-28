using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Manipulation; // for ConnectionPointPlace if needed later

class Program
{
    static void Main(string[] args)
    {
        // Expect input and optional output file paths
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if multiple pages are required)
            Page page = diagram.Pages[0];

            // Configure auto‑spacing options: horizontal and vertical gaps (in inches)
            AutoSpaceOptions spaceOptions = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // 0.5 inch horizontal spacing
                DistanceInVertical = 0.5    // 0.5 inch vertical spacing
            };

            // Apply auto‑spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, spaceOptions);

            // After spacing, recalculate routing for each connector (1‑D shape)
            foreach (Shape shape in page.Shapes)
            {
                // Identify connector shapes by the OneD flag
                if (shape.OneD)
                {
                    // Set routing style to right‑angle to avoid overlaps
                    shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                    // Optional: ensure connectors are allowed to reroute if needed
                    shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                }
            }

            // Save the updated diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}