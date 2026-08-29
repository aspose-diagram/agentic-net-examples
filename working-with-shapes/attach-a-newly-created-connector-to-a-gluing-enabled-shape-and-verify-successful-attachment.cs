using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Determine output file path (optional argument, default to "output.vsdx")
        string outputPath = args.Length > 0 ? args[0] : "output.vsdx";

        // Ensure the directory for the output file exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram with a default page
            Diagram diagram = new Diagram();

            // Access the first (and only) page in the diagram
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Add the first rectangle shape (gluing-enabled)
            // -----------------------------------------------------------------
            long rect1Id = page.AddShape(pinX: 2.0, pinY: 2.0, width: 2.0, height: 1.0, masterName: "Rectangle", isCalculate: false);
            Shape rect1 = page.Shapes.GetShape(rect1Id);
            // Enable dynamic glue on the rectangle so connectors can attach
            rect1.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

            // -----------------------------------------------------------------
            // Add the second rectangle shape (gluing-enabled)
            // -----------------------------------------------------------------
            long rect2Id = page.AddShape(pinX: 6.0, pinY: 2.0, width: 2.0, height: 1.0, masterName: "Rectangle", isCalculate: false);
            Shape rect2 = page.Shapes.GetShape(rect2Id);
            rect2.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

            // -----------------------------------------------------------------
            // Add a dynamic connector shape (initially unattached)
            // -----------------------------------------------------------------
            long connectorId = page.AddShape(pinX: 4.0, pinY: 2.0, width: 0.0, height: 0.0, masterName: "Dynamic connector", isCalculate: false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // -----------------------------------------------------------------
            // Attach the connector: glue rect1 bottom to rect2 top
            // -----------------------------------------------------------------
            // Use positional arguments as the method does not support named parameters
            page.ConnectShapesViaConnector(
                rect1Id,
                ConnectionPointPlace.Bottom,
                rect2Id,
                ConnectionPointPlace.Top,
                connectorId);

            // -----------------------------------------------------------------
            // Verify the connection via the page's Connects collection
            // -----------------------------------------------------------------
            bool connectionFound = false;
            foreach (Connect conn in page.Connects)
            {
                if (conn.FromSheet == rect1Id && conn.ToSheet == rect2Id && conn.FromCell.Contains("PinX") && conn.ToCell.Contains("PinX"))
                {
                    connectionFound = true;
                    break;
                }
            }

            // -----------------------------------------------------------------
            // Verify that the first rectangle reports the connector as glued
            // -----------------------------------------------------------------
            bool glueFound = false;
            long[] gluedIds = rect1.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);
            foreach (long id in gluedIds)
            {
                if (id == connectorId)
                {
                    glueFound = true;
                    break;
                }
            }

            // -----------------------------------------------------------------
            // Output verification results
            // -----------------------------------------------------------------
            if (connectionFound && glueFound)
            {
                Console.WriteLine("Connector successfully attached and verified.");
            }
            else
            {
                Console.Error.WriteLine("Failed to verify connector attachment.");
                if (!connectionFound) Console.Error.WriteLine("- Connection not found in page.Connects.");
                if (!glueFound) Console.Error.WriteLine("- Connector not reported as glued on the source shape.");
            }

            // -----------------------------------------------------------------
            // Save the diagram to the specified file
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}