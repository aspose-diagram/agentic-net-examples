using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Simulated external database: mapping shape IDs to tooltip text
                // In a real scenario, replace this with actual DB calls.
                Dictionary<long, string> tooltipData = new Dictionary<long, string>
                {
                    { 1, "Start Process" },
                    { 2, "Decision Point" },
                    { 3, "End Process" }
                    // Add more mappings as required
                };

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine tooltip text for the current shape
                        string tooltip;
                        if (!tooltipData.TryGetValue(shape.ID, out tooltip))
                        {
                            // If no specific tooltip, use a default or skip
                            tooltip = "No description available";
                        }

                        // Ensure the Hyperlinks collection exists
                        if (shape.Hyperlinks == null)
                        {
                            // The collection is always instantiated by Aspose.Diagram,
                            // but guard against null for safety.
                            continue;
                        }

                        // If the shape already has hyperlinks, update the first one's description.
                        // Otherwise, create a new hyperlink.
                        if (shape.Hyperlinks.Count > 0)
                        {
                            // Update description of the first hyperlink
                            shape.Hyperlinks[0].Description.Value = tooltip;
                        }
                        else
                        {
                            // Create a new hyperlink and set its description as the tooltip
                            Hyperlink link = new Hyperlink();
                            link.Description.Value = tooltip;
                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram tooltips have been updated and saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }