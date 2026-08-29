using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram to analyze
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            bool foundIssues = false;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Retrieve the connector's line jump style
                        var jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                        // If the style is PageDefault or Undefined, it means no explicit definition
                        if (jumpStyle == ConLineJumpStyleValue.PageDefault ||
                            jumpStyle == ConLineJumpStyleValue.Undefined)
                        {
                            foundIssues = true;
                            Console.WriteLine($"Connector ID {shape.ID} on page \"{page.Name}\" lacks explicit line jump style (Current: {jumpStyle}).");
                        }
                    }
                }
            }

            if (!foundIssues)
            {
                Console.WriteLine("All connectors have explicit line jump style definitions.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
