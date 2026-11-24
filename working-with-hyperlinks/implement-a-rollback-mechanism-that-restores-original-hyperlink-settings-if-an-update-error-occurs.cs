using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Simple DTO to store a snapshot of a hyperlink's properties
    private class HyperlinkSnapshot
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string SubAddress { get; set; }
        public string Description { get; set; }
    }

    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Dictionary to keep original hyperlink data for rollback (keyed by shape ID)
        var originalHyperlinks = new Dictionary<long, List<HyperlinkSnapshot>>();

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // STEP 1: Capture original hyperlink settings for every shape
            // -----------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Hyperlinks collection is present and not empty
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        var snapshotList = new List<HyperlinkSnapshot>();
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            var snap = new HyperlinkSnapshot
                            {
                                Name = link.Name,
                                Address = link.Address?.Value,
                                SubAddress = link.SubAddress?.Value,
                                Description = link.Description?.Value
                            };
                            snapshotList.Add(snap);
                        }
                        originalHyperlinks[shape.ID] = snapshotList;
                    }
                }
            }

            // -----------------------------------------------------------------
            // STEP 2: Perform hyperlink updates (example: append a query string)
            // -----------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Only modify if an address is present
                            if (link.Address != null && !string.IsNullOrEmpty(link.Address.Value))
                            {
                                // Example modification – append "?updated" to the URL
                                link.Address.Value = link.Address.Value + "?updated";
                            }
                        }
                    }
                }
            }

            // -----------------------------------------------------------------
            // STEP 3: Save the updated diagram
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // An error occurred – attempt to rollback to original hyperlink settings
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
            Console.Error.WriteLine("Attempting to restore original hyperlink data...");

            try
            {
                // Reload the original diagram to ensure a clean state for rollback
                Diagram rollbackDiagram = new Diagram(inputPath);

                // Restore saved hyperlink snapshots
                foreach (Page page in rollbackDiagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (originalHyperlinks.TryGetValue(shape.ID, out var savedLinks))
                        {
                            // Clear any existing hyperlinks on the shape
                            if (shape.Hyperlinks != null)
                            {
                                shape.Hyperlinks.Clear();
                            }

                            // Re‑add each saved hyperlink
                            foreach (var snap in savedLinks)
                            {
                                Hyperlink restoredLink = new Hyperlink();
                                restoredLink.Name = snap.Name;
                                if (restoredLink.Address != null)
                                    restoredLink.Address.Value = snap.Address;
                                if (restoredLink.SubAddress != null)
                                    restoredLink.SubAddress.Value = snap.SubAddress;
                                if (restoredLink.Description != null)
                                    restoredLink.Description.Value = snap.Description;

                                shape.Hyperlinks.Add(restoredLink);
                            }
                        }
                    }
                }

                // Save the rolled‑back diagram (overwrite the intended output)
                rollbackDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Rollback completed successfully.");
            }
            catch (Exception rollbackEx)
            {
                Console.Error.WriteLine($"Rollback failed: {rollbackEx.Message}");
            }
        }
    }
}