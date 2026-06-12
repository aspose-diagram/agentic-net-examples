using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output Visio file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Select connector shapes (1‑D shapes) using LINQ
                    var connectorShapes = page.Shapes
                                              .Cast<Shape>()
                                              .Where(s => s.OneD);

                    // Set each connector's routing style to orthogonal (right‑angle)
                    foreach (Shape connector in connectorShapes)
                    {
                        connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
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