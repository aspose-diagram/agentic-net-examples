using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a blank page
                diagram.Pages.Add(new Page());
                Page page = diagram.Pages[0];

                // ---------- Create shapes for three groups ----------
                // Group 1 shapes
                long g1ShapeId1 = page.AddShape(1.0, 1.0, "Rectangle", false);
                long g1ShapeId2 = page.AddShape(2.0, 1.0, "Rectangle", false);
                long g1ShapeId3 = page.AddShape(1.5, 2.0, "Rectangle", false);
                Shape g1Shape1 = page.Shapes.GetShape(g1ShapeId1);
                Shape g1Shape2 = page.Shapes.GetShape(g1ShapeId2);
                Shape g1Shape3 = page.Shapes.GetShape(g1ShapeId3);
                // Group them
                Shape group1 = page.Shapes.Group(new Shape[] { g1Shape1, g1Shape2, g1Shape3 });

                // Group 2 shapes
                long g2ShapeId1 = page.AddShape(4.0, 1.0, "Rectangle", false);
                long g2ShapeId2 = page.AddShape(5.0, 1.0, "Rectangle", false);
                long g2ShapeId3 = page.AddShape(4.5, 2.0, "Rectangle", false);
                Shape g2Shape1 = page.Shapes.GetShape(g2ShapeId1);
                Shape g2Shape2 = page.Shapes.GetShape(g2ShapeId2);
                Shape g2Shape3 = page.Shapes.GetShape(g2ShapeId3);
                Shape group2 = page.Shapes.Group(new Shape[] { g2Shape1, g2Shape2, g2Shape3 });

                // Group 3 shapes
                long g3ShapeId1 = page.AddShape(7.0, 1.0, "Rectangle", false);
                long g3ShapeId2 = page.AddShape(8.0, 1.0, "Rectangle", false);
                long g3ShapeId3 = page.AddShape(7.5, 2.0, "Rectangle", false);
                Shape g3Shape1 = page.Shapes.GetShape(g3ShapeId1);
                Shape g3Shape2 = page.Shapes.GetShape(g3ShapeId2);
                Shape g3Shape3 = page.Shapes.GetShape(g3ShapeId3);
                Shape group3 = page.Shapes.Group(new Shape[] { g3Shape1, g3Shape2, g3Shape3 });

                // ---------- Connect sub‑shapes from the three groups ----------
                // We'll connect the first shape of each group to the first shape of the next group
                List<long> connectorIds = new List<long>();

                // Connector 1: Group1 -> Group2
                long connectorId1 = page.AddShape(0, 0, "Dynamic connector", false);
                page.ConnectShapesViaConnector(g1ShapeId1, ConnectionPointPlace.Bottom, g2ShapeId1, ConnectionPointPlace.Top, connectorId1);
                connectorIds.Add(connectorId1);

                // Connector 2: Group2 -> Group3
                long connectorId2 = page.AddShape(0, 0, "Dynamic connector", false);
                page.ConnectShapesViaConnector(g2ShapeId1, ConnectionPointPlace.Bottom, g3ShapeId1, ConnectionPointPlace.Top, connectorId2);
                connectorIds.Add(connectorId2);

                // Connector 3: Group3 -> Group1
                long connectorId3 = page.AddShape(0, 0, "Dynamic connector", false);
                page.ConnectShapesViaConnector(g3ShapeId1, ConnectionPointPlace.Bottom, g1ShapeId1, ConnectionPointPlace.Top, connectorId3);
                connectorIds.Add(connectorId3);

                // ---------- List resulting connector IDs ----------
                Console.WriteLine("Connector shape IDs created:");
                foreach (long id in connectorIds)
                {
                    Console.WriteLine($"Connector ID: {id}");
                }

                // Save the diagram (optional)
                diagram.Save("ConnectedGroups.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }