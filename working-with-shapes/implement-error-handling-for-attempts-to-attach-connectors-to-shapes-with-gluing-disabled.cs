using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing diagram or create a new one
                Diagram diagram = new Diagram(); // empty diagram

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                    diagram.Pages.Add(new Page());

                Page page = diagram.Pages[0];

                // Add first shape (Rectangle master)
                long shapeId1 = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                Shape shape1 = page.Shapes.GetShape(shapeId1);

                // Add second shape (Rectangle master)
                long shapeId2 = diagram.AddShape(5.0, 5.0, "Rectangle", 0);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                // Add a dynamic connector shape
                long connectorId = diagram.AddShape(3.5, 3.5, "Dynamic connector", 0);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Disable dynamic glue on the first shape (simulate gluing disabled)
                shape1.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

                // Attempt to connect shapes with error handling
                try
                {
                    ConnectShapesWithGlueCheck(page, shape1, shape2, connector);
                    Console.WriteLine("Connector attached successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                // Save the diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        /// <summary>
        /// Connects two shapes via a connector after verifying that both shapes allow dynamic glue.
        /// Throws an exception if gluing is disabled on either shape.
        /// </summary>
        static void ConnectShapesWithGlueCheck(Page page, Shape fromShape, Shape toShape, Shape connectorShape)
        {
            // Check glue settings on the source shape
            if (fromShape.Misc.GlueType.Value == GlueTypeValue.NoAllowDynamicGlue)
                throw new Exception($"Gluing is disabled for source shape ID {fromShape.ID}.");

            // Check glue settings on the target shape
            if (toShape.Misc.GlueType.Value == GlueTypeValue.NoAllowDynamicGlue)
                throw new Exception($"Gluing is disabled for target shape ID {toShape.ID}.");

            // Both shapes allow glue; perform the connection
            page.ConnectShapesViaConnector(
                fromShape.ID,
                ConnectionPointPlace.Bottom,
                toShape.ID,
                ConnectionPointPlace.Top,
                connectorShape.ID);
        }
    }