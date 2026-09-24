using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Access the Layout property (example: change routing style)
                            shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                            // Modify the connector's position by setting PinX and PinY
                            // (coordinates are in inches)
                            shape.XForm.PinX.Value += 1.0; // move 1 inch to the right
                            shape.XForm.PinY.Value += 0.5; // move 0.5 inch up

                            Console.WriteLine($"Connector ID {shape.ID} moved to ({shape.XForm.PinX.Value}, {shape.XForm.PinY.Value})");
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