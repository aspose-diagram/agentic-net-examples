using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram (uses the provided creation rule)
            Diagram diagram = new Diagram();

            // Ensure there is at least one page; add a page if the diagram is empty
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Master shape name to use for adding shapes (e.g., a built‑in rectangle)
            string masterName = "Rectangle";

            // Batch add a shape to every page
            foreach (Page page in diagram.Pages)
            {
                // Add a shape at coordinates (1,1) inches on the page
                // Uses Diagram.Page.AddShape(double pinX, double pinY, string masterName)
                page.AddShape(1.0, 1.0, masterName);
            }

            // Validate that each page now contains at least one shape
            foreach (Page page in diagram.Pages)
            {
                if (page.Shapes.Count == 0)
                {
                    // If any page has no shapes, raise an exception
                    throw new InvalidOperationException($"Page '{page.Name}' does not contain any shapes.");
                }
            }

            // Save the diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
