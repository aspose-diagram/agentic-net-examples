using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (adjust as needed)
                Page page = diagram.Pages[0];

                // Ensure there is at least one shape to modify
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no shapes to enable gluing on.");
                    return;
                }

                // Retrieve the first shape (target for gluing)
                Shape targetShape = page.Shapes[0];
                long targetShapeId = targetShape.ID;

                // Enable dynamic gluing on the target shape
                // GlueTypeValue.AllowDynamicGlue permits connectors to glue automatically
                targetShape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

                // Add a dynamic connector shape to the page
                // Parameters: pinX, pinY, width, height, master name, isCalculate (bool)
                long connectorId = page.AddShape(0.0, 0.0, 0.0, 0.0, "Dynamic connector", false);

                // Glue the connector to the target shape at its center
                page.GlueShapes(targetShapeId, ConnectionPointPlace.Center, connectorId);

                // Save the modified diagram (VSDX format)
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