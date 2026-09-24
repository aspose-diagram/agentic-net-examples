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

                // Use the first page (default page is always present)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, master name, page index
                long shapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Record the initial number of connection points
                int initialCount = shape.Connections.Count;

                // Create a new connection point (center of the shape)
                Connection newConn = new Connection();
                newConn.X.Ufe.F = "Width*0.5";
                newConn.Y.Ufe.F = "Height*0";

                // Add the connection point to the shape
                shape.Connections.Add(newConn);

                // Verify that the count increased by one
                int finalCount = shape.Connections.Count;
                if (finalCount != initialCount + 1)
                {
                    throw new Exception($"Connection point count mismatch. Expected {initialCount + 1}, but got {finalCount}.");
                }
                else
                {
                    Console.WriteLine("Test passed: Adding a connection point increased the Connections count by one.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }