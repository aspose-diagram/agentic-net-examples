using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                // Replace "input.vsdx" with the actual file path
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Select connector shapes (1‑D shapes) that are not deleted
                    var connectors = page.Shapes
                                        .Where(shape => shape.OneD && shape.Del == BOOL.False);

                    // Apply orthogonal (right‑angle) routing style to each connector
                    foreach (Shape connector in connectors)
                    {
                        connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                    }
                }

                // Save the modified diagram
                // Replace "output.vsdx" with the desired output path
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }