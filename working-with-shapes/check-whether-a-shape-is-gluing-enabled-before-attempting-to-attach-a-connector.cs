using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Use the first page of the diagram
                Page page = diagram.Pages[0];

                // Add two rectangle shapes to the page
                long shapeId1 = page.AddShape(2.0, 5.0, "Rectangle");
                long shapeId2 = page.AddShape(6.0, 5.0, "Rectangle");

                // Retrieve the shape objects
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                // Verify that both shapes allow dynamic glue
                bool shape1GlueEnabled = shape1.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;
                bool shape2GlueEnabled = shape2.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;

                if (!shape1GlueEnabled || !shape2GlueEnabled)
                {
                    throw new Exception("One or both shapes are not gluing-enabled. Connector cannot be attached.");
                }

                // Add a dynamic connector shape
                long connectorId = page.AddShape(4.0, 5.0, "Dynamic connector");
                Shape connector = page.Shapes.GetShape(connectorId);

                // Optionally set connector routing style (right-angle)
                connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Connect the two shapes via the connector using bottom of shape1 and top of shape2
                page.ConnectShapesViaConnector(
                    shapeId1,
                    ConnectionPointPlace.Bottom,
                    shapeId2,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }