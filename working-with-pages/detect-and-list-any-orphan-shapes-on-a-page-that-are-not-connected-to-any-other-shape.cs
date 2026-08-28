using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to analyze
                string filePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.NameU} (ID: {page.ID})");

                    // Iterate through each shape on the current page
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
                            string shapeName = string.IsNullOrEmpty(shape.NameU) ? "(no name)" : shape.NameU;
                            Console.WriteLine($"  Orphan Shape - ID: {shape.ID}, NameU: {shapeName}");
                        }
                    }
                }

                // Dispose the diagram to release resources
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }