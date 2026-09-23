using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Create a new page and set its size (in inches)
        Page page = new Page();
        page.PageSheet.PageProps.PageWidth.Value = 11.0;   // Width = 11 inches
        page.PageSheet.PageProps.PageHeight.Value = 8.5;   // Height = 8.5 inches

        // Add the page to the diagram
        diagram.Pages.Add(page);

        // -----------------------------------------------------------------
        // 1. Create guide lines (using thin polyline shapes as guides)
        // -----------------------------------------------------------------
        // Example: vertical guide at X = 3 inches
        double guideX = 3.0;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
        long verticalGuideId = page.DrawPolyline(new double[] { guideX, 0.0, guideX, pageHeight });

        // Example: horizontal guide at Y = 2 inches
        double guideY = 2.0;
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        long horizontalGuideId = page.DrawPolyline(new double[] { 0.0, guideY, pageWidth, guideY });

        // Optionally, make the guides visually thin (line weight) and a light color
        Shape verticalGuide = page.Shapes.GetShape(verticalGuideId);
        verticalGuide.Line.LineWeight.Value = 0.01;               // Thin line
        verticalGuide.Line.LineColor.Value = "#CCCCCC";           // Light gray

        Shape horizontalGuide = page.Shapes.GetShape(horizontalGuideId);
        horizontalGuide.Line.LineWeight.Value = 0.01;
        horizontalGuide.Line.LineColor.Value = "#CCCCCC";

        // -----------------------------------------------------------------
        // 2. Add a rectangle shape that will be aligned to the guides
        // -----------------------------------------------------------------
        // Draw a rectangle (centered at (0,0) initially)
        double rectWidth = 2.0;   // 2 inches
        double rectHeight = 1.0;  // 1 inch
        long rectShapeId = page.DrawRectangle(0.0, 0.0, rectWidth, rectHeight);
        Shape rectShape = page.Shapes.GetShape(rectShapeId);

        // Align the rectangle's left edge to the vertical guide
        // PinX is the center X; to align left edge, subtract half the width
        rectShape.XForm.PinX.Value = guideX + rectWidth / 2.0;
        // Align the rectangle's top edge to the horizontal guide
        // PinY is the center Y; to align top edge, add half the height
        rectShape.XForm.PinY.Value = guideY - rectHeight / 2.0;

        // Optionally, give the rectangle a fill color
        rectShape.Fill.FillForegnd.Value = "#FFCC00"; // Orange fill
        rectShape.Fill.FillPattern.Value = 1;        // Solid fill

        // -----------------------------------------------------------------
        // 3. Save the diagram to a VSDX file
        // -----------------------------------------------------------------
        string outputPath = "GuidesAlignedDiagram.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine($"Diagram saved to '{outputPath}'.");
    }
}
