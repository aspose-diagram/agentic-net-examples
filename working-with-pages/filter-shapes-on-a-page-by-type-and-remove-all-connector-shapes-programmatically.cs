using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_without_connectors.vsdx";

                // Load the diagram from file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Collect IDs of connector shapes (1‑D shapes)
                        var connectorIds = new System.Collections.Generic.List<long>();

                        // Examine each shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // OneD is true for connector shapes
                            if (shape.OneD)
                            {
                                connectorIds.Add(shape.ID);
                            }
                        }

                        // Remove the identified connector shapes from the page
                        foreach (long id in connectorIds)
                        {
                            // Retrieve the shape instance by its ID
                            Shape connectorShape = page.Shapes.GetShape(id);
                            // Remove the shape from the page's shape collection
                            page.Shapes.Remove(connectorShape);
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Connector shapes removed and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }