using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Simple heuristic: count geometry elements as bends
                            // In Visio, each Geom segment can represent a bend.
                            int bendCount = shape.Geoms.Count;

                            // Apply reroute option only if there are more than three bends
                            if (bendCount > 3)
                            {
                                // Set the connector reroute option.
                                // Only ConFixedCodeValue.Undefined is valid in this API version.
                                shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                            }
                        }
                    }
                }

                // Save the modified diagram using a save format option
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }