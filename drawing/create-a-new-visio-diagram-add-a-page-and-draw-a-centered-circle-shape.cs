using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new Visio diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the active page (a default page is created automatically)
                Page page = diagram.ActivePage;

                // Draw a circle (ellipse with equal width and height) at an arbitrary position
                double pinX = 0;          // X coordinate of the shape's pin (center)
                double pinY = 0;          // Y coordinate of the shape's pin (center)
                double diameter = 2.0;    // Size of the circle
                page.DrawEllipse(pinX, pinY, diameter, diameter);

                // Center all shapes on the page
                page.CenterDrawing();

                // Save the diagram to a VDX file
                diagram.Save("CircleDiagram.vdx", SaveFileFormat.Vdx);
            }

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
