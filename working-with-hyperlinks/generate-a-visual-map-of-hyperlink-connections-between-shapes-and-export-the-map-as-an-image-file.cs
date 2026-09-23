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

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the source diagram
                Diagram diagram = new Diagram(sourcePath);

                // Use the first page for processing
                Page page = diagram.Pages[0];

                // Iterate all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without hyperlinks
                    if (shape.Hyperlinks == null || shape.Hyperlinks.Count == 0)
                        continue;

                    // Process each hyperlink attached to the shape
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Only handle internal links (SubAddress) that point to another shape
                        string subAddress = link.SubAddress?.Value;
                        if (string.IsNullOrEmpty(subAddress))
                            continue; // external URL – ignore for visual map

                        // Attempt to locate the target shape by its universal name (NameU)
                        Shape targetShape = null;
                        foreach (Shape candidate in page.Shapes)
                        {
                            if (candidate.NameU == subAddress)
                            {
                                targetShape = candidate;
                                break;
                            }
                        }

                        // If target not found, skip this link
                        if (targetShape == null)
                            continue;

                        // Create a dynamic connector shape (master name "Dynamic connector")
                        long connectorId = diagram.AddShape(0, 0, "Dynamic connector", 0);
                        Shape connector = page.Shapes.GetShape(connectorId);

                        // Connect the source shape to the target shape using the connector
                        // Use right side of source and left side of target as connection points
                        page.ConnectShapesViaConnector(
                            shape.ID,
                            ConnectionPointPlace.Right,
                            targetShape.ID,
                            ConnectionPointPlace.Left,
                            connectorId);
                    }
                }

                // Export the resulting diagram (with connectors) as a PNG image
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save("hyperlink_map.png", saveOptions);

                Console.WriteLine("Hyperlink connection map has been generated and saved as 'hyperlink_map.png'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }