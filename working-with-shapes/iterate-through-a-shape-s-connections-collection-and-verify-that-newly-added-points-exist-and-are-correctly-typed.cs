using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram (contains a default page)
                Diagram diagram = new Diagram();

                // Add a rectangle shape on the first page (page index 0)
                // Parameters: PinX, PinY, master name, page index
                long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

                // Retrieve the shape instance from the page's shape collection
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Add a new connection point to the shape
                // The X and Y values are expressed as fractions of the shape's width/height
                Connection newConn = new Connection();
                newConn.X.Value = 0.5; // middle of the shape width
                newConn.Y.Value = 0.0; // top edge
                shape.Connections.Add(newConn);

                // Verify that all connection points exist and are of the correct type
                foreach (Connection conn in shape.Connections)
                {
                    if (conn == null)
                    {
                        throw new Exception("Connection point is null.");
                    }

                    // X and Y should be DoubleValue instances
                    if (!(conn.X is DoubleValue))
                    {
                        throw new Exception("Connection X is not a DoubleValue.");
                    }

                    if (!(conn.Y is DoubleValue))
                    {
                        throw new Exception("Connection Y is not a DoubleValue.");
                    }

                    // Optionally, check that the values are within the expected range [0,1]
                    double xVal = conn.X.Value;
                    double yVal = conn.Y.Value;

                    if (xVal < 0.0 || xVal > 1.0 || yVal < 0.0 || yVal > 1.0)
                    {
                        throw new Exception($"Connection point has out-of-range coordinates: X={xVal}, Y={yVal}");
                    }

                    Console.WriteLine($"Connection point verified: X={xVal}, Y={yVal}");
                }

                // Save the diagram to verify that the changes persist
                diagram.Save("VerifiedDiagram.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }