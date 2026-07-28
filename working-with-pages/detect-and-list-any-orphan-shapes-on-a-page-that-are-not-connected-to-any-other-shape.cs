using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Get the Visio file path from command‑line arguments or prompt the user.
            string filePath;
            if (args.Length > 0)
            {
                filePath = args[0];
            }
            else
            {
                Console.Write("Enter Visio file path: ");
                filePath = Console.ReadLine();
            }

            // Load the diagram.
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.NameU} (ID: {page.ID})");
                bool foundOrphan = false;

                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve IDs of shapes connected to this shape.
                    long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                    // If there are no connections, the shape is an orphan.
                    if (connectedIds == null || connectedIds.Length == 0)
                    {
                        foundOrphan = true;
                        Console.WriteLine($"  Orphan Shape - ID: {shape.ID}, NameU: {shape.NameU}");
                    }
                }

                if (!foundOrphan)
                {
                    Console.WriteLine("  No orphan shapes on this page.");
                }
            }

            // Clean up resources.
            diagram.Dispose();

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
