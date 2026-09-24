using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                // Replace the path with the actual file location.
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Select connector shapes (1‑D shapes) using LINQ.
                    var connectorShapes = page.Shapes
                                              .Cast<Shape>()
                                              .Where(s => s.OneD);

                    // Set each connector's routing style to orthogonal (right‑angle).
                    foreach (Shape connector in connectorShapes)
                    {
                        connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                    }
                }

                // Save the modified diagram.
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }