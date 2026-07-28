using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Apply automatic spacing to all shapes on the page
            AutoSpaceOptions spacingOptions = new AutoSpaceOptions();
            spacingOptions.DistanceInHorizontal = 2; // inches
            spacingOptions.DistanceInVertical = 2;   // inches
            page.AutoSpaceShapes(page.Shapes, spacingOptions);

            // Verify that each connector is still attached to its source and target shapes
            foreach (Connect connection in page.Connects)
            {
                // Retrieve source and target shapes using their IDs
                Shape sourceShape = page.Shapes.GetShape(connection.FromSheet);
                Shape targetShape = page.Shapes.GetShape(connection.ToSheet);

                if (sourceShape == null)
                    throw new Exception($"Connector reference missing source shape with ID {connection.FromSheet}.");

                if (targetShape == null)
                    throw new Exception($"Connector reference missing target shape with ID {connection.ToSheet}.");

                // Ensure neither shape is marked as deleted
                if (sourceShape.Del == BOOL.True)
                    throw new Exception($"Source shape ID {sourceShape.ID} is marked as deleted.");

                if (targetShape.Del == BOOL.True)
                    throw new Exception($"Target shape ID {targetShape.ID} is marked as deleted.");

                // If the connector itself appears as a shape (OneD), verify it is not deleted
                Shape possibleConnector = page.Shapes.GetShape(connection.FromSheet);
                if (possibleConnector != null && possibleConnector.OneD)
                {
                    if (possibleConnector.Del == BOOL.True)
                        throw new Exception($"Connector shape ID {possibleConnector.ID} is marked as deleted.");
                }
            }

            Console.WriteLine("All connectors remain correctly attached after spacing.");

            // Optionally save the modified diagram
            diagram.Save("spaced_output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
