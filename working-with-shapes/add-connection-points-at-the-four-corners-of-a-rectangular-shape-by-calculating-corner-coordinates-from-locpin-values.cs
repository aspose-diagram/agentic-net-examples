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

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape at (5,5) with width 2 inches and height 1 inch
            // The AddShape method returns the shape ID (long)
            long rectId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle");

            // Retrieve the Shape object using the returned ID
            Shape rect = page.Shapes.GetShape(rectId);

            // ---- Add connection points at the four corners ----
            // Bottom‑Left corner (0,0)
            Connection cpBottomLeft = new Connection();
            cpBottomLeft.X.Ufe.F = "Width*0";
            cpBottomLeft.Y.Ufe.F = "Height*0";
            rect.Connections.Add(cpBottomLeft);

            // Bottom‑Right corner (Width,0)
            Connection cpBottomRight = new Connection();
            cpBottomRight.X.Ufe.F = "Width*1";
            cpBottomRight.Y.Ufe.F = "Height*0";
            rect.Connections.Add(cpBottomRight);

            // Top‑Left corner (0,Height)
            Connection cpTopLeft = new Connection();
            cpTopLeft.X.Ufe.F = "Width*0";
            cpTopLeft.Y.Ufe.F = "Height*1";
            rect.Connections.Add(cpTopLeft);

            // Top‑Right corner (Width,Height)
            Connection cpTopRight = new Connection();
            cpTopRight.X.Ufe.F = "Width*1";
            cpTopRight.Y.Ufe.F = "Height*1";
            rect.Connections.Add(cpTopRight);
            // --------------------------------------------------

            // Save the diagram to a VSDX file
            diagram.Save("RectangleWithCorners.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
