using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: DiagramGluingUtility <inputVisioFile> [outputVisioFile]");
                return;
            }

            string inputPath = args[0];
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if gluing is enabled for the shape
                    // GlueTypeValue.AllowDynamicGlue indicates that dynamic glue is allowed
                    if (shape.Misc.GlueType != null && shape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue)
                    {
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}, Page: {page.Name}");

                        // Retrieve IDs of connectors glued to this shape
                        long[] connectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);
                        if (connectorIds != null && connectorIds.Length > 0)
                        {
                            foreach (long connId in connectorIds)
                            {
                                // Retrieve the connector shape to obtain its name (if any)
                                Shape connector = page.Shapes.GetShape(connId);
                                string connectorName = connector != null ? connector.Name : "Unnamed";
                                Console.WriteLine($"\tAttached Connector ID: {connId}, Name: {connectorName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\tNo connectors attached.");
                        }
                    }
                }
            }

            // Save the diagram (unchanged) to demonstrate lifecycle usage
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }