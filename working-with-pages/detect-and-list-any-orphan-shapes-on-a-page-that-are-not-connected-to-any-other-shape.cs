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
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        Console.WriteLine($"Analyzing Page: {page.NameU}");

                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                            {
                                continue;
                            }

                            // Retrieve IDs of shapes connected to the current shape
                            long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                            // If there are no connections, the shape is an orphan
                            if (connectedIds == null || connectedIds.Length == 0)
                            {
                                Console.WriteLine($"Orphan Shape - ID: {shape.ID}, NameU: {shape.NameU}");
                            }
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }