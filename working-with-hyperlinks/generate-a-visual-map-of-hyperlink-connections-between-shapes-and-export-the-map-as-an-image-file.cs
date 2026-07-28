using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file containing shapes with hyperlinks
                string inputPath = "input.vsdx"; // TODO: replace with actual file path
                // Output image file path
                string outputPath = "hyperlink_map.png";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Build a lookup of shape universal names to their IDs
                Dictionary<string, long> shapeNameToId = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
                foreach (Shape shape in page.Shapes)
                {
                    if (!string.IsNullOrWhiteSpace(shape.NameU))
                    {
                        shapeNameToId[shape.NameU] = shape.ID;
                    }
                }

                // Iterate shapes and create connectors based on hyperlinks
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Hyperlinks == null) continue;

                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Use SubAddress to reference another shape by its universal name
                        string targetName = link.SubAddress?.Value;
                        if (string.IsNullOrWhiteSpace(targetName)) continue;

                        if (!shapeNameToId.TryGetValue(targetName, out long targetId)) continue;

                        // Add a dynamic connector shape (position will be adjusted automatically)
                        long connectorId = page.AddShape(0, 0, "Dynamic connector");
                        // Connect the source shape to the target shape via the connector
                        page.ConnectShapesViaConnector(
                            shape.ID,
                            ConnectionPointPlace.Bottom,
                            targetId,
                            ConnectionPointPlace.Top,
                            connectorId);
                    }
                }

                // Export the diagram (with added connectors) as a PNG image
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Hyperlink map exported to: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }