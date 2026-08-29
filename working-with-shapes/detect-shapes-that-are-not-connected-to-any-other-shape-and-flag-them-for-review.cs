using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve IDs of shapes connected to this shape
                        long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                        // If no connections found, flag the shape for review
                        if (connectedIds == null || connectedIds.Length == 0)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) is not connected to any other shape.");

                            // Add a custom property to indicate review needed
                            Prop reviewProp = new Prop();
                            reviewProp.Name = "ReviewFlag";
                            reviewProp.Label.Value = "Review Flag";
                            reviewProp.Value.Val = "True";
                            shape.Props.Add(reviewProp);
                        }
                    }
                }

                // Optionally, save the diagram with the added flags
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }