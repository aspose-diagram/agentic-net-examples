using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        using (Diagram diagram = new Diagram())
        {
            // Ensure at least one page exists
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Access the first page (page index 0)
            Page page = diagram.Pages[0];

            // Set all page margins to 0.5 inches
            page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
            page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
            page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
            page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;

            // Add shapes here after margins are configured
            // Example (commented out):
            // long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
            // Shape shape = page.Shapes.GetShape(shapeId);

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
