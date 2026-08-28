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

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add two rectangle shapes
                // Parameters: pinX, pinY, master name, isCalculate (bool)
                long shapeId1 = page.AddShape(2.0, 2.0, "Rectangle", false);
                long shapeId2 = page.AddShape(5.0, 5.0, "Rectangle", false);

                // Add a dynamic connector shape
                long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);

                // Retrieve the connector shape to modify its line style
                Shape connector = page.Shapes.GetShape(connectorId);
                // Set the line pattern to dashed
                connector.Line.LinePattern.Value = LinePatternValue.Dash;

                // Connect the two rectangles using the connector
                // Connect shape1 bottom to shape2 top
                page.ConnectShapesViaConnector(
                    shapeId1,
                    ConnectionPointPlace.Bottom,
                    shapeId2,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the diagram to a VSDX file
                diagram.Save("ConnectorDemo.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }