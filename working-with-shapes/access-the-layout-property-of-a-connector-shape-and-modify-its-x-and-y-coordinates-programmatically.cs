using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Find the first connector shape (1‑D shape)
                Shape? connector = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Connector shapes are 1‑D (OneD == true)
                    if (shape.OneD)
                    {
                        connector = shape;
                        break;
                    }
                }

                if (connector == null)
                {
                    throw new Exception("No connector shape found in the diagram.");
                }

                // Modify the connector's position via its XForm (PinX and PinY)
                // Example: move the connector to (5.0, 7.0) inches
                connector.XForm.PinX.Value = 5.0;
                connector.XForm.PinY.Value = 7.0;

                // Optionally, you can also adjust routing style via Layout if needed
                // connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Connector position updated and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }