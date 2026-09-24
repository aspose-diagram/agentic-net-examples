using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page (automatically created)
            Page page = diagram.Pages[0];

            // Define rectangle parameters
            double pinX = 5.0;   // center X coordinate (in inches)
            double pinY = 5.0;   // center Y coordinate (in inches)
            double width = 4.0;  // width of the rectangle (in inches)
            double height = 2.0; // height of the rectangle (in inches)

            // Add a rectangle shape; the last argument (isCalculate) must be false
            long rectId = page.AddShape(pinX, pinY, width, height, "Rectangle", false);
            Shape rect = page.Shapes.GetShape(rectId);

            // Bottom‑left corner connection point
            Connection cpBL = new Connection();
            cpBL.X.Ufe.F = "-LocPinX*Width";
            cpBL.Y.Ufe.F = "-LocPinY*Height";
            rect.Connections.Add(cpBL);

            // Bottom‑right corner connection point
            Connection cpBR = new Connection();
            cpBR.X.Ufe.F = "(1-LocPinX)*Width";
            cpBR.Y.Ufe.F = "-LocPinY*Height";
            rect.Connections.Add(cpBR);

            // Top‑left corner connection point
            Connection cpTL = new Connection();
            cpTL.X.Ufe.F = "-LocPinX*Width";
            cpTL.Y.Ufe.F = "(1-LocPinY)*Height";
            rect.Connections.Add(cpTL);

            // Top‑right corner connection point
            Connection cpTR = new Connection();
            cpTR.X.Ufe.F = "(1-LocPinX)*Width";
            cpTR.Y.Ufe.F = "(1-LocPinY)*Height";
            rect.Connections.Add(cpTR);

            // Save the diagram with the added connection points
            diagram.Save("RectangleWithCorners.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
