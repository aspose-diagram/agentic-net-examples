using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (replace with actual file path)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Connectors are 1‑D shapes
                        if (shape.OneD)
                        {
                            long connectorId = shape.ID;

                            // Retrieve the line jump style from the connector's layout
                            ConLineJumpStyleValue jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                            // Output the connector ID and its line jump style
                            Console.WriteLine($"Connector ID: {connectorId}, Line Jump Style: {jumpStyle}");
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }