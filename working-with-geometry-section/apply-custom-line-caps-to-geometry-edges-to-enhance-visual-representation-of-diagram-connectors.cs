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

                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Draw two rectangle shapes on the page
                // Parameters: pinX, pinY, width, height
                long rectId1 = page.DrawRectangle(2.0, 2.0, 2.0, 2.0);
                long rectId2 = page.DrawRectangle(6.0, 2.0, 2.0, 2.0);

                // Retrieve the shape objects for further manipulation (optional)
                Shape rect1 = page.Shapes.GetShape(rectId1);
                Shape rect2 = page.Shapes.GetShape(rectId2);

                // Add a dynamic connector shape to the diagram on page index 0
                // Parameters: pinX, pinY, master name, page index
                long connectorId = diagram.AddShape(4.0, 2.0, "Dynamic connector", 0);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Connect the two rectangles using the connector
                // Use bottom of the first rectangle and top of the second rectangle as connection points
                page.ConnectShapesViaConnector(
                    rectId1,
                    ConnectionPointPlace.Bottom,
                    rectId2,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Apply custom line caps to the connector
                // BOOL.True creates rounded caps; BOOL.False would create square caps
                connector.Line.LineCap.Value = BOOL.True;

                // Save the diagram to a VSDX file
                diagram.Save("CustomLineCaps.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }