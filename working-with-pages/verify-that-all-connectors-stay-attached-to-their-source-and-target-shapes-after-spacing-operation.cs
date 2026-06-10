using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file after spacing (optional)
                string outputPath = "output_spaced.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Configure auto‑spacing options
                AutoSpaceOptions spaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 1.0, // inches
                    DistanceInVertical = 1.0    // inches
                };

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, spaceOptions);

                // Verify that each connector is still attached to existing, non‑deleted shapes
                foreach (Connect connection in page.Connects)
                {
                    long sourceId = connection.FromSheet;
                    long targetId = connection.ToSheet;

                    // Retrieve source and target shapes
                    Shape sourceShape = page.Shapes.GetShape(sourceId);
                    Shape targetShape = page.Shapes.GetShape(targetId);

                    // Check that both shapes exist
                    if (sourceShape == null)
                    {
                        throw new Exception($"Connector (ID={connection.FromSheet}) references a missing source shape (ID={sourceId}).");
                    }

                    if (targetShape == null)
                    {
                        throw new Exception($"Connector (ID={connection.FromSheet}) references a missing target shape (ID={targetId}).");
                    }

                    // Ensure neither shape is marked as deleted
                    if (sourceShape.Del == BOOL.True)
                    {
                        throw new Exception($"Source shape (ID={sourceId}) of connector (ID={connection.FromSheet}) is marked as deleted.");
                    }

                    if (targetShape.Del == BOOL.True)
                    {
                        throw new Exception($"Target shape (ID={targetId}) of connector (ID={connection.FromSheet}) is marked as deleted.");
                    }
                }

                Console.WriteLine("All connectors remain correctly attached after spacing.");

                // Optional: save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }