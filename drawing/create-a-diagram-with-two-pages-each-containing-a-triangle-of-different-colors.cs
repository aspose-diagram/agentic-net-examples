using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // First page with a red triangle
        Page page1 = new Page();
        diagram.Pages.Add(page1);
        long redId = page1.DrawPolyline(new double[] { 2, 2, 4, 2, 3, 4, 2, 2 });
        Shape redTriangle = page1.Shapes.GetShape((int)redId);
        redTriangle.Fill.FillPattern.Value = 1;               // solid fill
        redTriangle.Fill.FillForegnd.Value = "#FF0000";       // red color
        redTriangle.Line.LineColor.Value = "#000000";        // black border

        // Second page with a green triangle
        Page page2 = new Page();
        diagram.Pages.Add(page2);
        long greenId = page2.DrawPolyline(new double[] { 2, 2, 5, 2, 3.5, 5, 2, 2 });
        Shape greenTriangle = page2.Shapes.GetShape((int)greenId);
        greenTriangle.Fill.FillPattern.Value = 1;             // solid fill
        greenTriangle.Fill.FillForegnd.Value = "#00FF00";     // green color
        greenTriangle.Line.LineColor.Value = "#000000";      // black border

        // Save the diagram to a VSDX file
        diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
