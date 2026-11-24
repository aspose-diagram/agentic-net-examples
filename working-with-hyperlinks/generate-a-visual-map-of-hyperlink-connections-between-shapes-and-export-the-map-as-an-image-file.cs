using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputImageFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Build a lookup of shape universal names to their IDs for quick hyperlink target resolution.
                Dictionary<string, long> nameToId = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Shape.NameU is a plain string (not a cell), so use it directly.
                        string shapeName = shape.NameU;
                        if (!string.IsNullOrEmpty(shapeName))
                        {
                            // If duplicate names exist, the last one wins – acceptable for this simple map.
                            nameToId[shapeName] = shape.ID;
                        }
                    }
                }

                // Iterate again to create visual connectors for each hyperlink.
                foreach (Page page in diagram.Pages)
                {
                    // Process each shape on the page.
                    foreach (Shape sourceShape in page.Shapes)
                    {
                        // Ensure the shape has a hyperlink collection.
                        if (sourceShape.Hyperlinks == null) continue;

                        // Enumerate hyperlinks using an explicit index‑based loop.
                        for (int h = 0; h < sourceShape.Hyperlinks.Count; h++)
                        {
                            Hyperlink hyperlink = sourceShape.Hyperlinks[h];

                            // Resolve the hyperlink address; treat it as a shape universal name.
                            string targetName = hyperlink.Address.Value;
                            if (string.IsNullOrEmpty(targetName)) continue;

                            // Find the target shape ID using the previously built dictionary.
                            if (!nameToId.TryGetValue(targetName, out long targetId)) continue;

                            // Add a connector shape of master "Dynamic connector".
                            // The AddShape overload returns the new shape ID.
                            long connectorId = diagram.AddShape(new Shape(), "Dynamic connector", page.ID);

                            // Connect the source shape to the target shape via the connector.
                            // Use the right side of the source and the left side of the target as default connection points.
                            page.ConnectShapesViaConnector(
                                sourceShape.ID,
                                ConnectionPointPlace.Right,
                                targetId,
                                ConnectionPointPlace.Left,
                                connectorId);
                        }
                    }
                }

                // Prepare image export options (PNG format).
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export the first page only; adjust if multi‑page export is required.
                    PageIndex = 0,
                    PageCount = 1,
                    // Optional: set a reasonable resolution.
                    Resolution = 300f
                };

                // Save the diagram as an image.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Hyperlink map image saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}