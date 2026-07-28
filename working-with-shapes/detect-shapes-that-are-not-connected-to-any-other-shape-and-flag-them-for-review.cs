using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the input Visio file path as the first argument.
                if (args.Length == 0)
                {
                    Console.WriteLine("Please provide the path to the Visio file as an argument.");
                    return;
                }

                string inputPath = args[0];
                string outputPath = System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(inputPath) ?? "",
                    System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_reviewed.vsdx");

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                bool foundIsolated = false;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve IDs of shapes connected to this shape.
                        long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                        // If there are no connections, flag the shape.
                        if (connectedIds == null || connectedIds.Length == 0)
                        {
                            Console.WriteLine($"Isolated shape detected - Page: '{page.NameU}', ID: {shape.ID}, NameU: '{shape.NameU}'");

                            // Add a custom property to mark the shape for review.
                            Prop reviewProp = new Prop();
                            reviewProp.Name = "ReviewFlag";
                            reviewProp.Value.Val = "True";
                            shape.Props.Add(reviewProp);

                            foundIsolated = true;
                        }
                    }
                }

                // Save the diagram with the added review flags if any isolated shapes were found.
                if (foundIsolated)
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved with review flags to: {outputPath}");
                }
                else
                {
                    Console.WriteLine("No isolated shapes were found.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }