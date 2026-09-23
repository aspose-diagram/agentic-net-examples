using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Set routing style to right‑angle for clearer routing
                            shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                            // Ensure connector jump style uses the default page setting
                            shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;

                            // Reset any explicit jump code to undefined
                            shape.Layout.ConLineJumpCode.Value = ConLineJumpCodeValue.Undefined;
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }