using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class ConnectionPointCopier
{
    /// <summary>
    /// Copies all connection points from a template shape to the specified target shapes.
    /// </summary>
    /// <param name="sourceFile">Path to the Visio file to load.</param>
    /// <param name="outputFile">Path where the modified Visio file will be saved.</param>
    /// <param name="templateShapeId">ID of the shape that contains the connection points to copy.</param>
    /// <param name="targetShapeIds">Collection of shape IDs that will receive the copied connection points.</param>
    public static void CopyConnectionPoints(string sourceFile, string outputFile, long templateShapeId, IEnumerable<long> targetShapeIds)
    {
        // Load the diagram using the provided load rule
        Diagram diagram = new Diagram(sourceFile);

        // Assume the diagram has at least one page; work with the first page
        Page page = diagram.Pages[0];

        // Retrieve the template shape that holds the original connection points
        Shape templateShape = page.Shapes.GetShape(templateShapeId);

        // Iterate over each target shape and copy the connection points
        foreach (long targetId in targetShapeIds)
        {
            Shape targetShape = page.Shapes.GetShape(targetId);

            // Optional: clear existing connection points on the target shape
            targetShape.Connections.Clear();

            // Clone each connection point from the template and add it to the target shape
            foreach (Connection conn in templateShape.Connections)
            {
                // Deep copy the connection point
                Connection newConn = conn.Clone() as Connection;

                // Assign a unique ID within the target shape's connection collection
                newConn.ID = targetShape.Connections.Count + 1;

                // Add the cloned connection point to the target shape
                targetShape.Connections.Add(newConn);
            }
        }

        // Save the modified diagram using the provided save rule
        diagram.Save(outputFile, SaveFileFormat.Vdx);
    }
}

// Example usage:
// ConnectionPointCopier.CopyConnectionPoints(
//     "input.vdx",
//     "output.vdx",
//     templateShapeId: 5,
//     targetShapeIds: new long[] { 10, 12, 15 });

class Program
{
    static void Main(string[] args)
    {
        try
        {

            ConnectionPointCopier.CopyConnectionPoints("", "", 0, null);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
