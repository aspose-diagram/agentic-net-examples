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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Perform auto‑layout (auto‑spacing) using FlowChart style.
                    LayoutOptions layoutOpts = new LayoutOptions
                    {
                        LayoutStyle = LayoutStyle.FlowChart,
                        EnlargePage = true // EnlargePage expects a bool, not BOOL.
                    };
                    page.Layout(layoutOpts);

                    // Recalculate routing for each connector (1‑D shape) on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes by the OneD flag.
                        if (shape.OneD)
                        {
                            // Ensure the shape has a Layout object before setting routing.
                            if (shape.Layout != null)
                            {
                                // Set the routing style to RightAngle to avoid overlaps.
                                shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                            }
                        }
                    }
                }

                // Save the updated diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
