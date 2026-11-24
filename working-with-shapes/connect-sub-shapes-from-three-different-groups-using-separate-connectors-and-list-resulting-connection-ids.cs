using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "ConnectedGroups.vsdx";

        try
        {
            // Create a new empty diagram inside a using block
            using (Diagram diagram = new Diagram())
            {
                // Get the first page (Visio diagrams always have at least one page)
                Page page = diagram.Pages[0];

                // -------------------------------------------------
                // Create sub‑shapes for three groups (each group has two rectangles)
                // -------------------------------------------------

                // Group 1
                long rect1Id = page.AddShape(1.0, 5.0, 2.0, 1.0, "Rectangle", false);
                long rect2Id = page.AddShape(3.0, 5.0, 2.0, 1.0, "Rectangle", false);
                Shape rect1 = page.Shapes.GetShape(rect1Id);
                Shape rect2 = page.Shapes.GetShape(rect2Id);
                Shape group1 = page.Shapes.Group(new Shape[] { rect1, rect2 });

                // Group 2
                long rect3Id = page.AddShape(1.0, 2.0, 2.0, 1.0, "Rectangle", false);
                long rect4Id = page.AddShape(3.0, 2.0, 2.0, 1.0, "Rectangle", false);
                Shape rect3 = page.Shapes.GetShape(rect3Id);
                Shape rect4 = page.Shapes.GetShape(rect4Id);
                Shape group2 = page.Shapes.Group(new Shape[] { rect3, rect4 });

                // Group 3
                long rect5Id = page.AddShape(1.0, -1.0, 2.0, 1.0, "Rectangle", false);
                long rect6Id = page.AddShape(3.0, -1.0, 2.0, 1.0, "Rectangle", false);
                Shape rect5 = page.Shapes.GetShape(rect5Id);
                Shape rect6 = page.Shapes.GetShape(rect6Id);
                Shape group3 = page.Shapes.Group(new Shape[] { rect5, rect6 });

                // -------------------------------------------------
                // Create three separate dynamic connectors
                // -------------------------------------------------
                long conn1Id = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);
                long conn2Id = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);
                long conn3Id = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);
                Shape conn1 = page.Shapes.GetShape(conn1Id);
                Shape conn2 = page.Shapes.GetShape(conn2Id);
                Shape conn3 = page.Shapes.GetShape(conn3Id);

                // Set connector style to straight lines (optional)
                conn1.SetConnectorsType(ConnectorsTypeValue.StraightLines);
                conn2.SetConnectorsType(ConnectorsTypeValue.StraightLines);
                conn3.SetConnectorsType(ConnectorsTypeValue.StraightLines);

                // -------------------------------------------------
                // Connect sub‑shapes from different groups using the connectors
                // -------------------------------------------------
                // Connect rect2 (Group1) to rect3 (Group2) using conn1
                page.ConnectShapesViaConnector(rect2Id, ConnectionPointPlace.Right, rect3Id, ConnectionPointPlace.Left, conn1Id);

                // Connect rect4 (Group2) to rect5 (Group3) using conn2
                page.ConnectShapesViaConnector(rect4Id, ConnectionPointPlace.Right, rect5Id, ConnectionPointPlace.Left, conn2Id);

                // Connect rect6 (Group3) to rect1 (Group1) using conn3
                page.ConnectShapesViaConnector(rect6Id, ConnectionPointPlace.Right, rect1Id, ConnectionPointPlace.Left, conn3Id);

                // -------------------------------------------------
                // List resulting connection IDs (FromSheet -> ToSheet)
                // -------------------------------------------------
                Console.WriteLine("Connections created:");
                foreach (Connect connection in page.Connects)
                {
                    Console.WriteLine($"Connector from Shape ID {connection.FromSheet} to Shape ID {connection.ToSheet}");
                }

                // -------------------------------------------------
                // Save the diagram to a VSDX file
                // -------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}