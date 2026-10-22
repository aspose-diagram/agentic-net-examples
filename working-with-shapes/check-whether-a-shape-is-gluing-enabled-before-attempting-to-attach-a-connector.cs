using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Find a source shape (first non‑connector shape)
                Shape sourceShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    // Skip connector shapes (OneD == true)
                    if (shp.OneD)
                        continue;

                    sourceShape = shp;
                    break;
                }

                // Find a target shape (next non‑connector shape different from source)
                Shape targetShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.OneD)
                        continue;

                    if (sourceShape != null && shp.ID == sourceShape.ID)
                        continue;

                    targetShape = shp;
                    break;
                }

                // Validate that both shapes were found
                if (sourceShape == null || targetShape == null)
                {
                    Console.Error.WriteLine("Could not locate two suitable shapes to connect.");
                    return;
                }

                // Check if the source shape allows dynamic gluing
                if (sourceShape.Misc.GlueType.Value != GlueTypeValue.AllowDynamicGlue)
                {
                    Console.WriteLine("Source shape is not gluing‑enabled. No connector will be added.");
                }
                else
                {
                    // Verify that the shapes are not already glued together
                    bool alreadyGlued = sourceShape.IsGlued(targetShape); // Changed to bool per API
                    if (alreadyGlued)
                    {
                        Console.WriteLine("The source shape is already glued to the target shape. No connector will be added.");
                    }
                    else
                    {
                        // Add a Dynamic Connector shape to the page (isCalculate = false)
                        long connectorId = page.AddShape(0, 0, "Dynamic connector", false);

                        // Connect the shapes via the newly added connector
                        page.ConnectShapesViaConnector(
                            sourceShape.ID,
                            ConnectionPointPlace.Center,
                            targetShape.ID,
                            ConnectionPointPlace.Center,
                            connectorId);

                        Console.WriteLine($"Connector (ID={connectorId}) added between shape {sourceShape.ID} and shape {targetShape.ID}.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
