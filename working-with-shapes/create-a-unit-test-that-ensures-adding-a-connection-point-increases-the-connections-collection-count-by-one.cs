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

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, master name, page index
                long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

                // Retrieve the shape object
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
                int afterCount = shape.Connections.Count;
                if (afterCount != initialCount + 1)
                {
                    throw new Exception($"Connection point count mismatch. Expected {initialCount + 1}, but got {afterCount}.");
                }

                Console.WriteLine("Test passed: Adding a connection point increased the Connections count by one.");

                // Optional: save the diagram to verify no errors during save
                diagram.Save("ConnectionPointTest.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }