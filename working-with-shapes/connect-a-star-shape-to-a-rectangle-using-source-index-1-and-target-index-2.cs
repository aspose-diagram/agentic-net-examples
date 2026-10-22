using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect stencil file path and output file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <stencilPath> <outputPath>");
            return;
        }

        string stencilPath = args[0];
        string outputPath = args[1];

        // Guard: ensure stencil file exists
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the stencil diagram which contains the required masters
            using (Diagram stencilDiagram = new Diagram(stencilPath))
            {
                // Create a new empty diagram
                using (Diagram diagram = new Diagram())
                {
                    // Copy masters from the stencil into the new diagram
                    foreach (Master master in stencilDiagram.Masters)
                    {
                        diagram.Masters.Add(master);
                    }

                    // Retrieve the first (and only) page of the new diagram
                    Page page = diagram.Pages[0];

                    // Add a star shape (master name "Star 7")
                    // Parameters: pinX, pinY, width, height, master name, isCalculate
                    long starShapeId = page.AddShape(1.0, 1.0, 2.0, 2.0, "Star 7", false);
                    Shape starShape = page.Shapes.GetShape(starShapeId);

                    // Add a rectangle shape (master name "Rectangle")
                    long rectShapeId = page.AddShape(5.0, 5.0, 2.0, 2.0, "Rectangle", false);
                    Shape rectShape = page.Shapes.GetShape(rectShapeId);

                    // Add a dynamic connector shape (master name "Dynamic connector")
                    long connectorId = page.AddShape(0.0, 0.0, 0.5, 0.5, "Dynamic connector", false);
                    Shape connectorShape = page.Shapes.GetShape(connectorId);

                    // Connect the star to the rectangle using connection indexes 1 (source) and 2 (target)
                    // The method signature: ConnectShapesViaConnectorIndex(sourceId, sourceIdx, targetId, targetIdx, connectorId)
                    page.ConnectShapesViaConnectorIndex(starShapeId, 1, rectShapeId, 2, connectorId);

                    // Save the resulting diagram to the specified output path in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}