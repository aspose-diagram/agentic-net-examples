using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file after spacing
                string outputPath = "spaced_output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Capture original connector relationships
                List<(long From, long To)> originalConnections = new List<(long From, long To)>();
                foreach (Connect conn in page.Connects)
                {
                    originalConnections.Add((conn.FromSheet, conn.ToSheet));
                }

                // Perform auto-spacing on all shapes on the page
                AutoSpaceOptions spaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2.0, // example horizontal spacing (inches)
                    DistanceInVertical = 2.0    // example vertical spacing (inches)
                };
                page.AutoSpaceShapes(page.Shapes, spaceOptions);

                // Verify that each connector still links the same source and target shapes
                int index = 0;
                foreach (Connect conn in page.Connects)
                {
                    if (index >= originalConnections.Count)
                    {
                        throw new Exception("Connector count increased after spacing operation.");
                    }

                    var original = originalConnections[index];
                    if (conn.FromSheet != original.From || conn.ToSheet != original.To)
                    {
                        throw new Exception($"Connector at index {index} changed its attachment: " +
                                            $"original From={original.From}, To={original.To}; " +
                                            $"now From={conn.FromSheet}, To={conn.ToSheet}");
                    }
                    index++;
                }

                if (index != originalConnections.Count)
                {
                    throw new Exception("Connector count decreased after spacing operation.");
                }

                Console.WriteLine("All connectors remain correctly attached after spacing.");

                // Save the modified diagram (optional)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }