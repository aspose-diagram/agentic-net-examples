using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram and add a page.
                Diagram diagram = new Diagram();
                diagram.Pages.Add(new Page());
                Page page = diagram.Pages[0];

                // Add two rectangle shapes.
                long shape1Id = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                long shape2Id = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

                // Add a dynamic connector shape.
                long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);

                // Retrieve shape objects for configuration.
                Shape shape1 = page.Shapes.GetShape(shape1Id);
                Shape shape2 = page.Shapes.GetShape(shape2Id);

                // Example: Disable gluing on the second shape.
                shape2.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

                try
                {
                    // Attempt to connect shapes with glue validation.
                    ConnectShapesWithGlueCheck(
                        page,
                        shape1Id,
                        shape2Id,
                        connectorId,
                        ConnectionPointPlace.Bottom,
                        ConnectionPointPlace.Top);
                    Console.WriteLine("Connector attached successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error attaching connector: {ex.Message}");
                }

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
        static void ConnectShapesWithGlueCheck(
            Page page,
            long fromShapeId,
            long toShapeId,
            long connectorId,
            ConnectionPointPlace fromPlace,
            ConnectionPointPlace toPlace)
        {
            // Retrieve the shape objects.
            Shape fromShape = page.Shapes.GetShape(fromShapeId);
            Shape toShape = page.Shapes.GetShape(toShapeId);

            // Check gluing settings on the source shape.
            if (fromShape.Misc.GlueType.Value == GlueTypeValue.NoAllowDynamicGlue)
            {
                throw new Exception($"Gluing is disabled on source shape (ID={fromShapeId}).");
            }

            // Check gluing settings on the target shape.
            if (toShape.Misc.GlueType.Value == GlueTypeValue.NoAllowDynamicGlue)
            {
                throw new Exception($"Gluing is disabled on target shape (ID={toShapeId}).");
            }

            // Both shapes allow gluing; perform the connection.
            page.ConnectShapesViaConnector(
                fromShapeId,
                fromPlace,
                toShapeId,
                toPlace,
                connectorId);
        }
    }