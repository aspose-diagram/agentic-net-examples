using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (avoid using ActivePage)
            Page page = diagram.Pages[0];

            // Retrieve the shape with ID 10
            long shapeId = 10L;
            Shape shape = page.Shapes.GetShape(shapeId);

            // Find the built‑in stylesheet named "Heading 1"
            StyleSheet headingStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Heading 1")
                {
                    headingStyle = ss;
                    break;
                }
            }

            if (headingStyle != null)
            {
                // Apply the stylesheet to the shape's text, fill, and line formatting
                shape.TextStyle = headingStyle;
                shape.FillStyle = headingStyle;
                shape.LineStyle = headingStyle;
            }
            else
            {
                // If the stylesheet is not found, report the issue
                Console.WriteLine("Error: 'Heading 1' stylesheet not found in the diagram.");
                return;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
