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

                // Iterate through all pages and shapes to find connectors (1‑D shapes)
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Connectors are 1‑D shapes (OneD == true)
                        if (shape.OneD)
                        {
                            // Retrieve the line jump style from the shape's layout cell
                            var jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                            // Log the connector ID and its current line jump style
                            Console.WriteLine($"Connector ID {shape.ID} line jump style: {jumpStyle}");
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