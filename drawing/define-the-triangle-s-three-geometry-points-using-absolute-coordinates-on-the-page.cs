using System.IO;
using System;
using Aspose.Diagram;

class TriangleExample
{
    static void Main()
    {
        // Create a new diagram (lifecycle rule: create)
        Diagram diagram = new Diagram();

        // Get the first page (default page is always present)
        Page page = diagram.Pages[0];

        // Define absolute coordinates for the three vertices of the triangle
        // Vertex A (100, 100)
        double ax = 100.0;
        double ay = 100.0;

        // Vertex B (300, 100)
        double bx = 300.0;
        double by = 100.0;

        // Vertex C (200, 250)
        double cx = 200.0;
        double cy = 250.0;

        // Draw the three sides of the triangle using absolute coordinates
        // Side AB
        page.DrawLine(ax, ay, bx, by);
        // Side BC
        page.DrawLine(bx, by, cx, cy);
        // Side CA
        page.DrawLine(cx, cy, ax, ay);

        // Save the diagram to a file (lifecycle rule: save)
        diagram.Save("TriangleDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
