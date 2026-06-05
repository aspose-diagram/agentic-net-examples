using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // ---------- Page 1 ----------
        Page page1 = new Page();
        page1.Name = "Page1";
        diagram.Pages.Add(page1);

        // Draw a red triangle on Page 1
        // First two points are passed directly, remaining points are in the array
        double[] extraPointsPage1 = new double[] { 3, 4, 2, 2 }; // third vertex and back to first to close
        long triangleId1 = page1.DrawPolyline(2, 2, 4, 2, extraPointsPage1);
        Shape triangle1 = page1.Shapes.GetShape((int)triangleId1);
        triangle1.Fill.FillPattern.Value = 1;               // solid fill
        triangle1.Fill.FillForegnd.Value = "#FF0000";       // red color
        triangle1.Line.LineColor.Value = "#000000";        // black outline

        // ---------- Page 2 ----------
        Page page2 = new Page();
        page2.Name = "Page2";
        diagram.Pages.Add(page2);

        // Draw a blue triangle on Page 2
        double[] extraPointsPage2 = new double[] { 3, 4, 2, 2 };
        long triangleId2 = page2.DrawPolyline(2, 2, 4, 2, extraPointsPage2);
        Shape triangle2 = page2.Shapes.GetShape((int)triangleId2);
        triangle2.Fill.FillPattern.Value = 1;               // solid fill
        triangle2.Fill.FillForegnd.Value = "#0000FF";       // blue color
        triangle2.Line.LineColor.Value = "#000000";        // black outline

        // Save the diagram to a VSDX file
        diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
