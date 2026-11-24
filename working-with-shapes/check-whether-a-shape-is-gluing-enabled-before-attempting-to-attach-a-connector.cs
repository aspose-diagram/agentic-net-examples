using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeNameOrId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdentifier = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Use the first page (index 0) for operations
                Page page = diagram.Pages[0];

                // Locate the target shape by ID (if numeric) or by universal name (NameU)
                Shape targetShape = null;
                if (long.TryParse(shapeIdentifier, out long shapeId))
                {
                    // Retrieve shape by ID
                    targetShape = page.Shapes.GetShape(shapeId);
                }
                else
                {
                    // Search for shape by its universal name (NameU)
                    foreach (Shape shp in page.Shapes)
                    {
                        if (shp.NameU == shapeIdentifier)
                        {
                            targetShape = shp;
                            break;
                        }
                    }
                }

                // Guard: ensure the shape was found
                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Error: Shape '{shapeIdentifier}' not found on the first page.");
                    return;
                }

                // -----------------------------------------------------------------
                // Determine whether the shape is gluing-enabled.
                // A shape is considered gluing-enabled when its GlueType is set to
                // AllowDynamicGlue. The GlueType cell resides in shape.Misc.
                // -----------------------------------------------------------------
                bool isGluingEnabled = false;

                if (targetShape.Misc != null && targetShape.Misc.GlueType != null &&
                    targetShape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue)
                {
                    isGluingEnabled = true;
                }

                Console.WriteLine($"Shape '{shapeIdentifier}' gluing-enabled: {isGluingEnabled}");

                // -----------------------------------------------------------------
                // If gluing-enabled, add a dynamic connector and glue it to the shape.
                // -----------------------------------------------------------------
                if (isGluingEnabled)
                {
                    // Add a dynamic connector shape to the page.
                    // The AddShape overload returns the new shape ID (long).
                    long connectorId = page.AddShape(0, 0, "Dynamic connector", false);

                    // Retrieve the connector shape object (optional, for further customization).
                    Shape connector = page.Shapes.GetShape(connectorId);

                    // Optionally set the connector's routing style to curved lines.
                    connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

                    // Glue the target shape to the connector at the connector's beginning point.
                    // GlueShapes(sourceShapeId, connectionPointPlace, targetShapeId)
                    page.GlueShapes(targetShape.ID, ConnectionPointPlace.Bottom, connectorId);

                    Console.WriteLine("Connector added and glued successfully.");
                }
                else
                {
                    Console.WriteLine("Connector not added because the shape is not gluing-enabled.");
                }

                // Save the modified diagram to a new file in VSDX format.
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? string.Empty,
                    "output.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}