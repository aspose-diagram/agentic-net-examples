using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (can be provided via command line or hard‑coded)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only connector shapes (1‑D shapes) that are not deleted
                        if (shape.OneD && shape.Del == BOOL.False)
                        {
                            // Set routing style to RightAngle for clearer routing
                            shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                            // Ensure the connector uses the default line‑jump style (no explicit jumps)
                            shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;

                            // Optionally, enforce straight connector type (overrides any existing type)
                            shape.SetConnectorsType(ConnectorsTypeValue.StraightLines);
                        }
                    }
                }

                // Save the modified diagram using a proper SaveOptions overload
                diagram.Save(outputPath, new DiagramSaveOptions(SaveFileFormat.Vsdx));

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }