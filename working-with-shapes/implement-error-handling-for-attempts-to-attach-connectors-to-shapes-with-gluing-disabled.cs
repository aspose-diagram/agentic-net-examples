using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram (empty Visio document)
                Diagram diagram = new Diagram();

                // Ensure there is at least one page to work with
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Reference the first page
                Page page = diagram.Pages[0];

                // Add two rectangle shapes (using a built‑in master name)
                // Parameters: pinX, pinY, masterName, pageIndex
                long shapeId1 = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
                long shapeId2 = diagram.AddShape(5.0, 5.0, "Rectangle", 0);

                // Retrieve the shape objects for further inspection
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);

                // Add a dynamic connector shape
                long connectorId = diagram.AddShape(0.0, 0.0, "Dynamic connector", 0);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Function to verify whether a shape allows dynamic glue
                bool IsGluingEnabled(Shape s)
                {
                    // GlueTypeValue.AllowDynamicGlue means glue is enabled
                    // GlueTypeValue.NoAllowDynamicGlue means glue is disabled
                    return s.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;
                }

                // Check glue settings for both source and target shapes
                if (!IsGluingEnabled(shape1))
                {
                    throw new Exception($"Gluing is disabled for shape ID {shape1.ID} (source shape).");
                }

                if (!IsGluingEnabled(shape2))
                {
                    throw new Exception($"Gluing is disabled for shape ID {shape2.ID} (target shape).");
                }

                // If both shapes allow gluing, attach the connector
                // Use ConnectionPointPlace.Bottom for the source and ConnectionPointPlace.Top for the target
                page.ConnectShapesViaConnector(shapeId1, ConnectionPointPlace.Bottom,
                                              shapeId2, ConnectionPointPlace.Top,
                                              connectorId);

                // Save the diagram to a VSDX file
                string outputPath = "ConnectorGluingDemo.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved successfully to " + outputPath);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }