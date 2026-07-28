using System;
using System.Globalization;
using Aspose.Diagram;

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

                // Add a rectangle shape at position (5,5) with size 4x2 inches
                // The fourth parameter (isCalculate) must be a bool
                long rectId = page.AddShape(5.0, 5.0, 4.0, 2.0, "Rectangle", false);

                // Retrieve the shape object using the returned ID
                Shape rect = page.Shapes.GetShape(rectId);

                // Extract necessary geometry values
                double pinX = rect.XForm.PinX.Value;
                double pinY = rect.XForm.PinY.Value;
                double width = rect.XForm.Width.Value;
                double height = rect.XForm.Height.Value;
                double locPinX = rect.XForm.LocPinX.Value;
                double locPinY = rect.XForm.LocPinY.Value;

                // Calculate corner coordinates based on LocPin values
                // Top‑Left
                double tlX = pinX - locPinX;
                double tlY = pinY + (height - locPinY);
                // Top‑Right
                double trX = pinX + (width - locPinX);
                double trY = tlY;
                // Bottom‑Left
                double blX = tlX;
                double blY = pinY - locPinY;
                // Bottom‑Right
                double brX = trX;
                double brY = blY;

                // Helper to add a connection point at a given coordinate
                void AddConnection(double x, double y)
                {
                    Connection cp = new Connection();
                    cp.X.Ufe.F = x.ToString(CultureInfo.InvariantCulture);
                    cp.Y.Ufe.F = y.ToString(CultureInfo.InvariantCulture);
                    rect.Connections.Add(cp);
                }

                // Add connection points at the four corners
                AddConnection(tlX, tlY);
                AddConnection(trX, trY);
                AddConnection(blX, blY);
                AddConnection(brX, brY);

                // Save the diagram to a VSDX file
                diagram.Save("RectangleWithCorners.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }