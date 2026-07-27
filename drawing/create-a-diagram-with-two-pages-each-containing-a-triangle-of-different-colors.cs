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

        // Add first page
        Page page1 = new Page();
        page1.Name = "Page1";
        diagram.Pages.Add(page1);

        // Add second page
        Page page2 = new Page();
        page2.Name = "Page2";
        diagram.Pages.Add(page2);

        // Define triangle vertices (in inches) and close the shape by repeating the first point
        double[] trianglePoints = new double[] { 2, 2, 4, 2, 3, 4, 2, 2 };

        // Draw triangle on the first page and set its fill color to red
        long shapeId1 = page1.DrawPolyline(trianglePoints);
        Shape triangle1 = page1.Shapes.GetShape(shapeId1);
        triangle1.Fill.FillForegnd.Value = "#FF0000"; // Red fill
        triangle1.Fill.FillPattern.Value = 1;        // Solid fill

        // Draw triangle on the second page and set its fill color to green
        long shapeId2 = page2.DrawPolyline(trianglePoints);
        Shape triangle2 = page2.Shapes.GetShape(shapeId2);
        triangle2.Fill.FillForegnd.Value = "#00FF00"; // Green fill
        triangle2.Fill.FillPattern.Value = 1;        // Solid fill

        // Save the diagram to a VSDX file
        diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
