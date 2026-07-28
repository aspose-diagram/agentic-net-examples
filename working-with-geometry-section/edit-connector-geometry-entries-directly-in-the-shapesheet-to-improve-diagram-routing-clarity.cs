using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Set connector routing style to RightAngle for clearer routing
                            shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                            // Force square line jumps at intersections
                            shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Square;

                            // Ensure no automatic line jump code overrides (use Undefined)
                            shape.Layout.ConLineJumpCode.Value = ConLineJumpCodeValue.Undefined;

                            // Prevent automatic rerouting of this connector
                            shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }