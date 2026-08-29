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
                using (Diagram diagram = new Diagram())
                {
                    // Get the first (and only) page
                    Page page = diagram.Pages[0];

                    // -------------------------------------------------
                    // Create sub‑shapes that will later be placed inside separate groups
                    // -------------------------------------------------
                    // Sub‑shape 1 (rectangle)
                    long subShapeId1 = page.DrawRectangle(2.0, 2.0, 1.0, 1.0);
                    Shape subShape1 = page.Shapes.GetShape(subShapeId1);

                    // An extra shape to make the first group non‑trivial
                    long extraShapeId1 = page.DrawEllipse(2.0, 4.0, 0.5, 0.5);
                    Shape extraShape1 = page.Shapes.GetShape(extraShapeId1);

                    // Group 1 containing subShape1 and extraShape1
                    Shape group1 = page.Shapes.Group(new Shape[] { subShape1, extraShape1 });

                    // Sub‑shape 2 (rectangle)
                    long subShapeId2 = page.DrawRectangle(8.0, 2.0, 1.0, 1.0);
                    Shape subShape2 = page.Shapes.GetShape(subShapeId2);

                    // An extra shape for the second group
                    long extraShapeId2 = page.DrawEllipse(8.0, 4.0, 0.5, 0.5);
                    Shape extraShape2 = page.Shapes.GetShape(extraShapeId2);

                    // Group 2 containing subShape2 and extraShape2
                    Shape group2 = page.Shapes.Group(new Shape[] { subShape2, extraShape2 });

                    // -------------------------------------------------
                    // Create a dynamic connector shape
                    // -------------------------------------------------
                    // The master name "Dynamic connector" is part of the default Visio stencil.
                    long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);
                    Shape connector = page.Shapes.GetShape(connectorId);

                    // Optional: set a routing style for the connector (right‑angle)
                    connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                    // -------------------------------------------------
                    // Connect the two sub‑shapes (which reside in different groups)
                    // -------------------------------------------------
                    // Use ConnectionPointPlace.Bottom for the source and ConnectionPointPlace.Top for the target
                    page.ConnectShapesViaConnector(
                        subShape1.ID,
                        ConnectionPointPlace.Bottom,
                        subShape2.ID,
                        ConnectionPointPlace.Top,
                        connectorId);

                    // -------------------------------------------------
                    // Save the diagram to a VSDX file
                    // -------------------------------------------------
                    string outputPath = "ConnectedGroups.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }