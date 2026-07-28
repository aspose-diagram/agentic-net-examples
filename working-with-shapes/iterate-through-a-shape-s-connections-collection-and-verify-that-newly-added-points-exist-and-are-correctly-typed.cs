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

                // Add a rectangle shape to the first page (page index 0)
                // PinX = 2, PinY = 2, master name = "Rectangle"
                long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Add a new connection point to the shape
                Connection newConn = new Connection();
                // Position the connection point at the center of the shape
                newConn.X.Ufe.F = "Width*0.5";
                newConn.Y.Ufe.F = "Height*0.5";
                shape.Connections.Add(newConn);

                // Verify that all connection points exist and have valid X/Y values
                foreach (Connection conn in shape.Connections)
                {
                    // X and Y should not be null
                    if (conn.X == null)
                    {
                        throw new Exception("Connection point missing X coordinate.");
                    }
                    if (conn.Y == null)
                    {
                        throw new Exception("Connection point missing Y coordinate.");
                    }

                    // The formula strings should not be empty
                    string xFormula = conn.X.Ufe.F;
                    string yFormula = conn.Y.Ufe.F;

                    if (string.IsNullOrWhiteSpace(xFormula))
                    {
                        throw new Exception("Connection point X coordinate has an empty formula.");
                    }
                    if (string.IsNullOrWhiteSpace(yFormula))
                    {
                        throw new Exception("Connection point Y coordinate has an empty formula.");
                    }

                    // Optionally, output the verified connection point
                    Console.WriteLine($"Verified connection point: X = {xFormula}, Y = {yFormula}");
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