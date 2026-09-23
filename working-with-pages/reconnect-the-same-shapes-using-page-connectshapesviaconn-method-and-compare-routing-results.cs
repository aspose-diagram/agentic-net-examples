using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: path to the source Visio file.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the provided file.
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page (assumes at least one page exists).
            Page page = diagram.Pages[0];

            // Locate two non‑connector shapes to be linked.
            long shapeId1 = -1, shapeId2 = -1;
            foreach (Shape s in page.Shapes)
            {
                // Skip connector shapes (OneD == true) and deleted shapes.
                if (s.OneD || s.Del == BOOL.True) continue;

                if (shapeId1 == -1)
                    shapeId1 = s.ID;
                else if (shapeId2 == -1)
                {
                    shapeId2 = s.ID;
                    break;
                }
            }

            // Validate that two shapes were found.
            if (shapeId1 == -1 || shapeId2 == -1)
            {
                Console.Error.WriteLine("Unable to locate two suitable shapes for connection.");
                return;
            }

            // Locate an existing connector shape (OneD == true).
            long connectorId = -1;
            foreach (Shape s in page.Shapes)
            {
                if (s.OneD && s.Del == BOOL.False)
                {
                    connectorId = s.ID;
                    break;
                }
            }

            // If no connector exists, create one using the built‑in "Dynamic connector" master.
            if (connectorId == -1)
            {
                // Attempt to add a connector; this requires the master to be available in the diagram.
                // The master name "Dynamic connector" is standard in Visio stencil libraries.
                connectorId = page.AddShape(1.0, 1.0, "Dynamic connector", false);
            }

            // Retrieve the connector shape instance for later property changes.
            Shape connector = page.Shapes.GetShape(connectorId);

            // Helper to perform connection, set routing, save files, and log results.
            void ApplyRouting(ConnectorsTypeValue routing, string suffix)
            {
                // Re‑connect the shapes using the specified connector.
                page.ConnectShapesViaConnector(shapeId1, ConnectionPointPlace.Right, shapeId2, ConnectionPointPlace.Bottom, connectorId);

                // Apply the desired routing style to the connector.
                connector.SetConnectorsType(routing);

                // Log the routing style applied.
                Console.WriteLine($"Routing set to {routing} (saved as *{suffix}*)");

                // Save the diagram in VSDX format.
                string vsdxPath = $"output_{suffix}.vsdx";
                diagram.Save(vsdxPath, SaveFileFormat.Vsdx);

                // Export a PNG image of the first page for visual comparison.
                string pngPath = $"output_{suffix}.png";
                ImageSaveOptions imgOpts = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(pngPath, imgOpts);
            }

            // Apply StraightLines routing and save.
            ApplyRouting(ConnectorsTypeValue.StraightLines, "straight");

            // Apply RightAngle routing and save.
            ApplyRouting(ConnectorsTypeValue.RightAngle, "rightangle");

            // Apply CurvedLines routing and save.
            ApplyRouting(ConnectorsTypeValue.CurvedLines, "curved");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and report them.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}