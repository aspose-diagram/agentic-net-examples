using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram or create a new one.
                // Here we create a new empty diagram.
                Diagram diagram = new Diagram();

                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Use the first page.
                Page page = diagram.Pages[0];

                // Add a rectangle shape.
                // Parameters: PinX, PinY, Width, Height, MasterName
                // The rectangle will be centered at (5,5) inches with size 2x1 inches.
                long rectId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle");

                // Retrieve the shape object.
                Shape rect = page.Shapes.GetShape(rectId);

                // Add connection points at the four corners.
                // Corner formulas are based on LocPinX/Y, Width, and Height.
                // Top‑Left corner
                Connection topLeft = new Connection();
                topLeft.X.Ufe.F = "-LocPinX";
                topLeft.Y.Ufe.F = "-LocPinY";
                rect.Connections.Add(topLeft);

                // Top‑Right corner
                Connection topRight = new Connection();
                topRight.X.Ufe.F = "Width - LocPinX";
                topRight.Y.Ufe.F = "-LocPinY";
                rect.Connections.Add(topRight);

                // Bottom‑Left corner
                Connection bottomLeft = new Connection();
                bottomLeft.X.Ufe.F = "-LocPinX";
                bottomLeft.Y.Ufe.F = "Height - LocPinY";
                rect.Connections.Add(bottomLeft);

                // Bottom‑Right corner
                Connection bottomRight = new Connection();
                bottomRight.X.Ufe.F = "Width - LocPinX";
                bottomRight.Y.Ufe.F = "Height - LocPinY";
                rect.Connections.Add(bottomRight);

                // Save the diagram to a VSDX file.
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }