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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Define guide positions (in inches)
            double verticalGuideX = 2.0;   // vertical guide at 2 inches from the left edge
            double horizontalGuideY = 3.0; // horizontal guide at 3 inches from the top edge

            // -------------------------------------------------
            // Shape 1: Rectangle centered on both guides
            // -------------------------------------------------
            // Add a rectangle master shape and place its center (PinX, PinY) on the guide intersection
            long rectId = diagram.AddShape(verticalGuideX, horizontalGuideY, "Rectangle", 0);
            Shape rect = page.Shapes.GetShape(rectId);

            // Ensure the shape is not marked as deleted
            if (rect.Del == BOOL.True) rect.Del = BOOL.False;

            // Set shape text
            rect.Text.Value.Clear();
            rect.Text.Value.Add(new Txt("Aligned Rectangle"));

            // -------------------------------------------------
            // Shape 2: Ellipse aligned to the top‑left of the guides
            // -------------------------------------------------
            double ellipseWidth = 1.5;   // width in inches
            double ellipseHeight = 1.0;  // height in inches

            // Calculate PinX/PinY so that the ellipse's top‑left corner touches the guides
            double ellipsePinX = verticalGuideX + ellipseWidth / 2.0;   // center X
            double ellipsePinY = horizontalGuideY - ellipseHeight / 2.0; // center Y

            long ellipseId = diagram.AddShape(ellipsePinX, ellipsePinY, ellipseWidth, ellipseHeight, "Ellipse", 0);
            Shape ellipse = page.Shapes.GetShape(ellipseId);

            if (ellipse.Del == BOOL.True) ellipse.Del = BOOL.False;

            ellipse.Text.Value.Clear();
            ellipse.Text.Value.Add(new Txt("Top‑Left Aligned"));

            // -------------------------------------------------
            // Save the diagram with the aligned shapes
            // -------------------------------------------------
            diagram.Save("GuidesAligned.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
