using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string outputPath = "ConnectedDiagram.vsdx";

        // Guard: ensure the directory for the output file exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block as required
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page to work with
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Diagram does not contain any pages.");
                    return;
                }

                // Use the first page
                Page page = diagram.Pages[0];

                // Add a rectangle shape
                // Parameters: pinX, pinY, master name, isCalculate (bool)
                long rectangleId = page.AddShape(2.0, 2.0, "Rectangle", false);
                // Retrieve the rectangle shape object for any further modifications (optional)
                Shape rectangle = page.Shapes.GetShape(rectangleId);

                // Add a star shape (using the "Star 7" master)
                long starId = page.AddShape(5.0, 5.0, "Star 7", false);
                Shape star = page.Shapes.GetShape(starId);

                // Add a dynamic connector shape
                Shape connectorShape = new Shape();
                long connectorId = diagram.AddShape(connectorShape, "Dynamic connector", 0);
                // Retrieve the connector shape object (optional)
                Shape connector = diagram.Pages[0].Shapes.GetShape(connectorId);

                // Connect the star to the rectangle using connection indexes
                // Source index = 1, Target index = 2 as per the task requirement
                page.ConnectShapesViaConnectorIndex(starId, 1, rectangleId, 2, connectorId);

                // Save the diagram to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}