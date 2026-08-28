using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Access the first (and only) page
                Page page = diagram.Pages[0];

                // -------------------------------------------------
                // 1. Add two rectangle shapes that will be connected
                // -------------------------------------------------
                // DrawRectangle(pinX, pinY, width, height) returns the shape ID (long)
                long rect1Id = page.DrawRectangle(1.0, 1.0, 2.0, 1.0);
                long rect2Id = page.DrawRectangle(5.0, 1.0, 2.0, 1.0);

                // Retrieve the shape objects (optional, for further styling)
                Shape rect1 = page.Shapes.GetShape(rect1Id);
                Shape rect2 = page.Shapes.GetShape(rect2Id);

                // -------------------------------------------------
                // 2. Add a dynamic connector shape
                // -------------------------------------------------
                // AddShape(pinX, pinY, masterName) creates a shape from a master.
                // "Dynamic connector" is a built‑in master in the default stencil.
                long connectorId = page.AddShape(3.0, 1.0, "Dynamic connector");
                Shape connector = page.Shapes.GetShape(connectorId);

                // -------------------------------------------------
                // 3. Connect the rectangles using the connector
                // -------------------------------------------------
                // Use ConnectionPointPlace from Aspose.Diagram.Manipulation
                page.ConnectShapesViaConnector(
                    rect1Id,
                    ConnectionPointPlace.Right,
                    rect2Id,
                    ConnectionPointPlace.Left,
                    connectorId);

                // -------------------------------------------------
                // 4. Apply custom line caps to the connector
                // -------------------------------------------------
                // BOOL.True creates rounded caps; BOOL.False creates square caps.
                connector.Line.LineCap.Value = BOOL.True; // Rounded line ends

                // Optionally, adjust other line properties for visibility
                connector.Line.LineColor.Value = "#FF0000"; // Red line color
                connector.Line.LineWeight.Value = 0.03;    // Thickness in inches

                // -------------------------------------------------
                // 5. Save the diagram to a VSDX file
                // -------------------------------------------------
                string outputPath = "ConnectorWithLineCaps.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }