using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Set page size (optional, here 11 x 8.5 inches)
            page.PageSheet.PageProps.PageWidth.Value = 11.0;
            page.PageSheet.PageProps.PageHeight.Value = 8.5;

            // -----------------------------------------------------------------
            // 1. Create visual guide lines using thin connector shapes (lines)
            // -----------------------------------------------------------------

            // Horizontal guide at Y = 5 inches (from X = 0 to X = 10)
            long horizGuideId = page.DrawPolyline(new double[] { 0.0, 5.0, 10.0, 5.0 });
            Shape horizGuide = page.Shapes.GetShape(horizGuideId);
            horizGuide.Line.LineWeight.Value = 0.01;               // thin line
            horizGuide.Line.LineColor.Value = "#C0C0C0";           // light gray
            horizGuide.Line.LinePattern.Value = LinePatternValue.Solid;

            // Vertical guide at X = 5 inches (from Y = 0 to Y = 10)
            long vertGuideId = page.DrawPolyline(new double[] { 5.0, 0.0, 5.0, 10.0 });
            Shape vertGuide = page.Shapes.GetShape(vertGuideId);
            vertGuide.Line.LineWeight.Value = 0.01;
            vertGuide.Line.LineColor.Value = "#C0C0C0";
            vertGuide.Line.LinePattern.Value = LinePatternValue.Solid;

            // -----------------------------------------------------------------
            // 2. Add a shape that will be aligned to the guides
            // -----------------------------------------------------------------
            // Add a rectangle master shape (width 2", height 1") roughly at (0,0)
            long rectId = page.AddShape(0.0, 0.0, 2.0, 1.0, "Rectangle", false);
            Shape rectangle = page.Shapes.GetShape(rectId);

            // Align the rectangle's center (PinX, PinY) to the intersection of the guides
            rectangle.XForm.PinX.Value = 5.0;   // X coordinate of vertical guide
            rectangle.XForm.PinY.Value = 5.0;   // Y coordinate of horizontal guide

            // Optionally, give the rectangle a visible fill and border
            rectangle.Fill.FillForegnd.Value = "#FFCC00";   // orange fill
            rectangle.Fill.FillPattern.Value = 1;          // solid fill pattern (integer)
            rectangle.Line.LineColor.Value = "#000000";    // black border
            rectangle.Line.LineWeight.Value = 0.02;

            // Add some text to the rectangle
            rectangle.Text.Value.Clear();
            rectangle.Text.Value.Add(new Txt("Aligned Box"));

            // -----------------------------------------------------------------
            // 3. Save the diagram to a VSDX file
            // -----------------------------------------------------------------
            diagram.Save("GuidesAlignedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}