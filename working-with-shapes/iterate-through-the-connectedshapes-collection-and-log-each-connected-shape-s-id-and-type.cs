using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve IDs of shapes connected to the current shape
                        long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                        if (connectedIds == null || connectedIds.Length == 0)
                            continue; // No connected shapes for this shape

                        // Log each connected shape's ID and type
                        foreach (long connId in connectedIds)
                        {
                            // Retrieve the connected shape by its ID
                            Shape connectedShape = page.Shapes.GetShape(connId);
                            if (connectedShape == null)
                                continue; // Safety check

                            Console.WriteLine($"Shape ID: {connectedShape.ID}, Type: {connectedShape.Type}");
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