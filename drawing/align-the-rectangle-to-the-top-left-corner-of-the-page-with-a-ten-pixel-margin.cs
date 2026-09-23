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

            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Convert a 10‑pixel margin to inches (assuming 96 DPI)
            double marginInches = 10.0 / 96.0;

            // Desired rectangle size (in inches)
            double rectWidth = 2.0;
            double rectHeight = 1.0;

            // Calculate the PinX and PinY so the rectangle's top‑left corner sits at the margin
            double pinX = marginInches + rectWidth / 2.0;
            double pinY = page.PageSheet.PageProps.PageHeight.Value - marginInches - rectHeight / 2.0;

            // Add a rectangle shape using the built‑in master "Rectangle"
            long shapeId = page.AddShape(pinX, pinY, "Rectangle", false);

            // Retrieve the shape to adjust its dimensions if necessary
            Shape rect = page.Shapes.GetShape(shapeId);
            rect.XForm.Width.Value = rectWidth;
            rect.XForm.Height.Value = rectHeight;

            // Save the diagram
            diagram.Save("AlignedRectangle.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
