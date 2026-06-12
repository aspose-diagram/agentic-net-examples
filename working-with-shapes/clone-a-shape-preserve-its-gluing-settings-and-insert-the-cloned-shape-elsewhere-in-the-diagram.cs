using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Identify the shape to clone (example: shape with ID 1)
            // In a real scenario, replace this with the actual shape ID or search logic.
            long originalShapeId = 1;
            Shape originalShape = page.Shapes.GetShape(originalShapeId);
            if (originalShape == null)
            {
                throw new Exception($"Shape with ID {originalShapeId} not found.");
            }

            // Retrieve the master name of the original shape
            if (originalShape.Master == null)
            {
                throw new Exception("Original shape does not have an associated master.");
            }
            string masterName = originalShape.Master.Name;

            // Determine new position for the cloned shape (offset by 2 inches on X axis)
            double newPinX = originalShape.XForm.PinX.Value + 2.0;
            double newPinY = originalShape.XForm.PinY.Value;

            // Add a new shape based on the same master
            long clonedShapeId = page.AddShape(newPinX, newPinY, masterName);
            Shape clonedShape = page.Shapes.GetShape(clonedShapeId);
            if (clonedShape == null)
            {
                throw new Exception("Failed to create cloned shape.");
            }

            // Copy visual and property data from the original shape to the cloned shape
            originalShape.Copy(clonedShape);

            // Preserve gluing (connections) by replicating Connect objects
            // Iterate over existing connections on the page
            foreach (Connect conn in page.Connects)
            {
                bool involvesOriginal = conn.FromSheet == originalShapeId || conn.ToSheet == originalShapeId;
                if (!involvesOriginal)
                    continue;

                // Create a new connection for the cloned shape
                Connect newConn = new Connect();

                // Replace the original shape ID with the cloned shape ID where applicable
                newConn.FromSheet = (conn.FromSheet == originalShapeId) ? clonedShapeId : conn.FromSheet;
                newConn.FromCell = conn.FromCell;
                newConn.ToSheet = (conn.ToSheet == originalShapeId) ? clonedShapeId : conn.ToSheet;
                newConn.ToCell = conn.ToCell;

                // Add the new connection to the page
                page.Connects.Add(newConn);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
