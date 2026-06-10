using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Determine the Visio file path
            string filePath;
            if (args.Length > 0)
            {
                filePath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the Visio file: ");
                filePath = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("No file path provided. Exiting.");
                return;
            }

            // Load the diagram using the Aspose.Diagram constructor
            using (Diagram diagram = new Diagram(filePath))
            {
                bool anyOrphans = false;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve IDs of shapes connected to this shape
                        long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                        // If there are no connections, the shape is an orphan
                        if (connectedIds == null || connectedIds.Length == 0)
                        {
                            anyOrphans = true;
                            Console.WriteLine($"Orphan Shape - Page: \"{page.Name}\" (ID {page.ID}), Shape ID: {shape.ID}, Name: \"{shape.Name}\", NameU: \"{shape.NameU}\"");
                        }
                    }
                }

                if (!anyOrphans)
                {
                    Console.WriteLine("No orphan shapes were found in the diagram.");
                }
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
