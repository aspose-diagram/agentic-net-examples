using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one page; add a blank page if none exist
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Batch add a rectangle shape to every page
        foreach (Page page in diagram.Pages)
        {
            // Draw a rectangle at (1,1) with width 2 and height 1 (in inches)
            long shapeId = page.DrawRectangle(1.0, 1.0, 2.0, 1.0);

            // Retrieve the shape to set its text (optional)
            Shape shape = page.Shapes.GetShape(shapeId);
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample"));
        }

        // Validate that each page contains at least one shape
        foreach (Page page in diagram.Pages)
        {
            if (page.Shapes.Count == 0)
            {
                throw new Exception($"Validation failed: Page '{page.Name}' contains no shapes.");
            }
            else
            {
                Console.WriteLine($"Page '{page.Name}' contains {page.Shapes.Count} shape(s).");
            }
        }

        // Save the diagram to a VSDX file
        diagram.Save("BatchAddedDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
