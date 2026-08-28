using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Verify the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the diagram inside a try/catch to capture any Aspose errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Ensure the diagram has at least one page
        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("Diagram contains no pages.");
            return;
        }

        // Work with the first page (you can adapt to other pages if needed)
        Page page = diagram.Pages[0];

        // Helper method to build a map of connector IDs to their source and target shape IDs
        Dictionary<long, (long source, long target)> BuildConnectorMap()
        {
            var map = new Dictionary<long, (long source, long target)>();
            // Iterate all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Identify connector shapes (1‑D shapes)
                if (shape.OneD)
                {
                    long connectorId = shape.ID;
                    long sourceId = -1;
                    long targetId = -1;

                    // Scan the page's Connect collection to find connections involving this connector
                    foreach (Connect conn in page.Connects)
                    {
                        if (conn.FromSheet == connectorId)
                        {
                            // This connection points from the connector to another shape (target)
                            targetId = conn.ToSheet;
                        }
                        else if (conn.ToSheet == connectorId)
                        {
                            // This connection points from another shape to the connector (source)
                            sourceId = conn.FromSheet;
                        }
                    }

                    // Store the discovered source/target pair (may be -1 if not connected)
                    map[connectorId] = (sourceId, targetId);
                }
            }
            return map;
        }

        // Capture connector relationships before the spacing operation
        var beforeMap = BuildConnectorMap();

        // Perform automatic spacing of shapes on the page
        try
        {
            // Configure spacing options (default distances are used here)
            var spacingOptions = new AutoSpaceOptions
            {
                DistanceInHorizontal = 1.0, // horizontal gap in inches
                DistanceInVertical = 1.0    // vertical gap in inches
            };
            // Apply spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, spacingOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during auto‑spacing: {ex.Message}");
            return;
        }

        // Capture connector relationships after the spacing operation
        var afterMap = BuildConnectorMap();

        // Compare before/after maps and report any mismatches
        bool allConnected = true;
        foreach (var kvp in beforeMap)
        {
            long connectorId = kvp.Key;
            var before = kvp.Value;
            var after = afterMap.ContainsKey(connectorId) ? afterMap[connectorId] : (source: -1L, target: -1L);

            // If either source or target changed, report the discrepancy
            if (before.source != after.source || before.target != after.target)
            {
                allConnected = false;
                Console.WriteLine($"Connector ID {connectorId} lost its attachment:");
                Console.WriteLine($"  Before -> Source: {before.source}, Target: {before.target}");
                Console.WriteLine($"  After  -> Source: {after.source}, Target: {after.target}");
            }
        }

        // Final result output
        if (allConnected)
        {
            Console.WriteLine("All connectors remain correctly attached to their source and target shapes after spacing.");
        }
        else
        {
            Console.WriteLine("Some connectors were detached or re‑attached incorrectly after spacing.");
        }
    }
}