using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.NameU}");

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve IDs of shapes connected to this shape
                        long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                        // If there are no connections, flag the shape for review
                        if (connectedIds == null || connectedIds.Length == 0)
                        {
                            // Example flagging: output shape details to console
                            Console.WriteLine($"  Unconnected Shape - ID: {shape.ID}, NameU: {shape.NameU}");

                            // Optionally, add a comment to the shape to mark it for review
                            // Note: AddComment adds a comment visible in Visio
                            page.AddComment(shape.ID, "Unconnected shape - review required");
                        }
                    }
                }

                // Optionally, save the diagram with comments added
                string outputPath = "output_review.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }