using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Verify that a file path argument is provided
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: GluingInspector <VisioFilePath>");
                    return;
                }

                string filePath = args[0];

                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name} (ID: {page.ID})");

                    // Iterate through all shapes on the current page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Check if the shape has dynamic glue enabled
                        if (shape.Misc.GlueType != null && shape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue)
                        {
                            Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}, NameU: {shape.NameU}");

                            // Retrieve IDs of all 1-D connector shapes glued to this shape
                            long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                            if (gluedConnectorIds != null && gluedConnectorIds.Length > 0)
                            {
                                Console.WriteLine("    Attached Connectors:");
                                foreach (long connectorId in gluedConnectorIds)
                                {
                                    // Retrieve the connector shape by its ID
                                    Shape connectorShape = page.Shapes.GetShape(connectorId);
                                    if (connectorShape != null)
                                    {
                                        Console.WriteLine($"      Connector ID: {connectorShape.ID}, Name: {connectorShape.Name}, NameU: {connectorShape.NameU}");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("    No connectors attached.");
                            }
                        }
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }