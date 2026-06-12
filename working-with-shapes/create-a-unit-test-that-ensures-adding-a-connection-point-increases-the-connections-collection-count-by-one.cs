using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first page (default page is always present)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page.
                // Parameters: PinX, PinY, master name, page index
                long shapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);

                // Retrieve the shape object from the page
                Shape shape = page.Shapes.GetShape(shapeId);

                // Record the initial number of connection points
                int initialCount = shape.Connections.Count;

                // Create a new connection point
                Connection newConn = new Connection();
                // Position the connection point at the middle of the top edge
                newConn.X.Ufe.F = "Width*0.5";
                newConn.Y.Ufe.F = "Height*0";

                // Add the connection point to the shape
                shape.Connections.Add(newConn);

                // Record the count after adding
                int afterCount = shape.Connections.Count;

                // Verify that the count increased by exactly one
                if (afterCount != initialCount + 1)
                {
                    throw new Exception($"Test failed: expected connection count {initialCount + 1}, but got {afterCount}.");
                }
                else
                {
                    Console.WriteLine("Test passed: adding a connection point increased the Connections count by one.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }