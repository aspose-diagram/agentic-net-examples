using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "Output_Glued.vsdx");

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Locate a group shape on the page
                Shape groupShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        groupShape = shape;
                        break;
                    }
                }

                if (groupShape == null)
                {
                    Console.Error.WriteLine("No group shape found on the first page.");
                    return;
                }

                // Enable dynamic gluing for the group shape
                // (Allow the group to accept glued connectors)
                groupShape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

                // Locate another non‑group shape to connect with the group
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ID != groupShape.ID && shape.Type != TypeValue.Group)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No suitable target shape found to connect with the group.");
                    return;
                }

                // Add a dynamic connector shape to the page
                // Position (0,0) with a small size; isCalculate = false
                long connectorId = page.AddShape(0.0, 0.0, 0.5, 0.5, "Dynamic connector", false);
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Connect the group shape and the target shape using the shared connector
                // Glue both ends of the connector to the center of each shape
                page.ConnectShapesViaConnector(
                    groupShape.ID,
                    ConnectionPointPlace.Center,
                    targetShape.ID,
                    ConnectionPointPlace.Center,
                    connectorShape.ID);

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}