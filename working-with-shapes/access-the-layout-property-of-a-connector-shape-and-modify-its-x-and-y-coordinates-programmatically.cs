using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define new coordinates for the connector (in inches)
                double newPinX = 5.0;
                double newPinY = 3.0;

                // Iterate through pages to locate the first connector shape (1‑D shape)
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Connectors are 1‑D shapes; check the OneD flag
                        if (shape.OneD)
                        {
                            // Access the Layout property (required by the task)
                            Layout connectorLayout = shape.Layout;

                            // Although Layout does not hold position cells, we modify the
                            // geometric position via the XForm property, which controls PinX and PinY.
                            shape.XForm.PinX.Value = newPinX;
                            shape.XForm.PinY.Value = newPinY;

                            Console.WriteLine($"Connector shape ID {shape.ID} moved to PinX={newPinX}, PinY={newPinY}.");

                            // If you need to adjust layout‑specific settings, you can do it here,
                            // e.g., connectorLayout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                            // Exit after modifying the first connector
                            goto SaveDiagram;
                        }
                    }
                }

                SaveDiagram:
                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }