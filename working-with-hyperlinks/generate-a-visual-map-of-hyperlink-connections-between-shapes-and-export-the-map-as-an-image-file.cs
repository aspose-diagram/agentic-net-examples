using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file containing shapes with hyperlinks
                string inputPath = "input.vsdx";
                // Output image file that will contain the visual map
                string outputPath = "hyperlink_map.png";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Collect all shapes on the page for quick lookup by NameU
                    var shapeLookup = new System.Collections.Generic.Dictionary<string, Shape>();
                    foreach (Shape shp in page.Shapes)
                    {
                        if (!string.IsNullOrEmpty(shp.NameU))
                        {
                            shapeLookup[shp.NameU] = shp;
                        }
                    }

                    // Iterate again to process hyperlinks
                    foreach (Shape sourceShape in page.Shapes)
                    {
                        if (sourceShape.Hyperlinks == null)
                            continue;

                        foreach (Hyperlink link in sourceShape.Hyperlinks)
                        {
                            // Consider only internal hyperlinks (SubAddress)
                            string subAddr = link.SubAddress?.Value;
                            if (string.IsNullOrEmpty(subAddr))
                                continue;

                            // SubAddress may be in the form "PageName!ShapeName" or just "ShapeName"
                            string targetShapeName = subAddr;
                            if (subAddr.Contains("!"))
                            {
                                // Split and ignore page part for this simple example
                                var parts = subAddr.Split('!');
                                if (parts.Length == 2)
                                    targetShapeName = parts[1];
                            }

                            // Find the target shape on the same page
                            if (!shapeLookup.TryGetValue(targetShapeName, out Shape targetShape))
                            {
                                Console.WriteLine($"Target shape '{targetShapeName}' not found on page '{page.Name}'.");
                                continue;
                            }

                            // Add a connector shape (Dynamic connector) to the page
                            long connectorId = diagram.AddShape(0, 0, "Dynamic connector", pageIndex);
                            // Connect source shape to target shape using the connector
                            page.ConnectShapesViaConnector(
                                sourceShape.ID,
                                ConnectionPointPlace.Bottom,
                                targetShape.ID,
                                ConnectionPointPlace.Top,
                                connectorId);
                        }
                    }
                }

                // Export the diagram (with added connectors) as a PNG image
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Hyperlink connection map saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }