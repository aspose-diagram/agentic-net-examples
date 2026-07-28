using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (provide via command line or use defaults)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // ------------------------------------------------------------
                // 1. Locate the group shape (by universal name "Group1")
                // ------------------------------------------------------------
                Shape groupShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group && shape.NameU == "Group1")
                    {
                        groupShape = shape;
                        break;
                    }
                }

                if (groupShape == null)
                    throw new Exception("Group shape with NameU 'Group1' was not found on the page.");

                // ------------------------------------------------------------
                // 2. Enable dynamic glue on the group shape
                // ------------------------------------------------------------
                // Allow other shapes (including connectors) to glue to this group
                groupShape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

                // ------------------------------------------------------------
                // 3. Add a target shape (e.g., a rectangle) to connect to
                // ------------------------------------------------------------
                long targetShapeId = page.AddShape(8.0, 5.0, "Rectangle");
                Shape targetShape = page.Shapes.GetShape(targetShapeId);

                // ------------------------------------------------------------
                // 4. Add a shared connector (dynamic connector)
                // ------------------------------------------------------------
                // Position the connector roughly between the two shapes
                long connectorId = page.AddShape(6.5, 5.0, "Dynamic connector");
                Shape connector = page.Shapes.GetShape(connectorId);

                // Optional: set connector routing style (straight line)
                connector.SetConnectorsType(ConnectorsTypeValue.StraightLines);

                // ------------------------------------------------------------
                // 5. Connect the group shape to the target shape using the connector
                // ------------------------------------------------------------
                // Use bottom of the group and top of the target shape as connection points
                page.ConnectShapesViaConnector(
                    groupShape.ID,
                    ConnectionPointPlace.Bottom,
                    targetShape.ID,
                    ConnectionPointPlace.Top,
                    connectorId);

                // ------------------------------------------------------------
                // 6. Save the modified diagram
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }