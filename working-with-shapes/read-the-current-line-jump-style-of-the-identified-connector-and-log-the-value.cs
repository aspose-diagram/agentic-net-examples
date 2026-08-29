using System.IO;
using System;
using Aspose.Diagram;

using Aspose.Diagram.Saving; // Required for shape operations per global rule

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Find a connector shape (1‑D shape). If you know the connector ID, you can retrieve it directly.
            Shape connector = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD) // Connectors are 1‑D shapes
                    {
                        connector = shape;
                        break;
                    }
                }
                if (connector != null)
                    break;
            }

            if (connector == null)
            {
                Console.WriteLine("No connector shape found in the diagram.");
                return;
            }

            // Read the current line jump style of the connector
            var jumpStyle = connector.Layout.ConLineJumpStyle.Value; // ConLineJumpStyleValue enum

            // Log the value
            Console.WriteLine($"Connector ID {connector.ID} line jump style: {jumpStyle}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
