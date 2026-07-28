using System;
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

                // Use the first (default) page
                Page page = diagram.Pages[0];

                // -----------------------------------------------------------------
                // Add shapes that will become members of three separate groups
                // -----------------------------------------------------------------
                // Group 1 shapes
                long shape1Id = page.AddShape(1.0, 1.0, "Rectangle");
                long shape2Id = page.AddShape(2.0, 1.0, "Ellipse");

                // Group 2 shapes
                long shape3Id = page.AddShape(4.0, 1.0, "Rectangle");
                long shape4Id = page.AddShape(5.0, 1.0, "Ellipse");

                // Group 3 shapes
                long shape5Id = page.AddShape(7.0, 1.0, "Rectangle");
                long shape6Id = page.AddShape(8.0, 1.0, "Ellipse");

                // Retrieve Shape objects for grouping
                Shape shape1 = page.Shapes.GetShape(shape1Id);
                Shape shape2 = page.Shapes.GetShape(shape2Id);
                Shape shape3 = page.Shapes.GetShape(shape3Id);
                Shape shape4 = page.Shapes.GetShape(shape4Id);
                Shape shape5 = page.Shapes.GetShape(shape5Id);
                Shape shape6 = page.Shapes.GetShape(shape6Id);

                // -----------------------------------------------------------------
                // Create three groups, each containing two shapes
                // -----------------------------------------------------------------
                Shape group1 = page.Shapes.Group(new Shape[] { shape1, shape2 });
                Shape group2 = page.Shapes.Group(new Shape[] { shape3, shape4 });
                Shape group3 = page.Shapes.Group(new Shape[] { shape5, shape6 });

                // -----------------------------------------------------------------
                // Add three separate dynamic connectors
                // -----------------------------------------------------------------
                long connector1Id = page.AddShape(3.0, 2.0, "Dynamic connector");
                long connector2Id = page.AddShape(6.0, 2.0, "Dynamic connector");
                long connector3Id = page.AddShape(9.0, 2.0, "Dynamic connector");

                // -----------------------------------------------------------------
                // Connect sub‑shapes from the groups using the connectors
                //   Connector 1: shape1 (Group1) -> shape3 (Group2)
                //   Connector 2: shape3 (Group2) -> shape5 (Group3)
                //   Connector 3: shape5 (Group3) -> shape1 (Group1)
                // -----------------------------------------------------------------
                page.ConnectShapesViaConnector(
                    shape1.ID, ConnectionPointPlace.Bottom,
                    shape3.ID, ConnectionPointPlace.Top,
                    connector1Id);

                page.ConnectShapesViaConnector(
                    shape3.ID, ConnectionPointPlace.Bottom,
                    shape5.ID, ConnectionPointPlace.Top,
                    connector2Id);

                page.ConnectShapesViaConnector(
                    shape5.ID, ConnectionPointPlace.Bottom,
                    shape1.ID, ConnectionPointPlace.Top,
                    connector3Id);

                // -----------------------------------------------------------------
                // List resulting connection IDs (FromSheet -> ToSheet)
                // -----------------------------------------------------------------
                Console.WriteLine("Connections created:");
                foreach (Connect conn in page.Connects)
                {
                    Console.WriteLine($"FromSheet (Connector ID): {conn.FromSheet}, ToSheet (Shape ID): {conn.ToSheet}");
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