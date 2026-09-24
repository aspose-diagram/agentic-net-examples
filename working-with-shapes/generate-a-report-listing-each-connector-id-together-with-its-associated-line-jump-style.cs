using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (replace with your actual file path)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages and shapes to find connectors
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Connectors are 1‑D shapes (OneD == true)
                        if (shape.OneD)
                        {
                            long connectorId = shape.ID;
                            // Retrieve the line jump style from the layout sub‑object
                            var jumpStyle = shape.Layout.ConLineJumpStyle.Value;
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