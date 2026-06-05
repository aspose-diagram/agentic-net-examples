using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new Visio diagram
        using (Diagram diagram = new Diagram())
        {
            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Define size for the circle (ellipse with equal width and height)
            double width = 2.0;   // width in inches (Visio uses inches by default)
            double height = 2.0;  // height in inches

            // Draw the ellipse at the origin; it will be centered later
            page.DrawEllipse(0, 0, width, height);

            // Center all shapes on the page
            page.CenterDrawing();

            // Save the diagram to a VDX file using DiagramSaveOptions
            DiagramSaveOptions saveOptions = new DiagramSaveOptions();
            diagram.Save("CircleDiagram.vdx", saveOptions);
        }
    }
}
