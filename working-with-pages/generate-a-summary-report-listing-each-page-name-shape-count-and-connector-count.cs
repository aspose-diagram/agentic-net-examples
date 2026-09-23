using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with actual file path)
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        int totalShapes = page.Shapes.Count;
                        int connectorCount = 0;

                        // Count connector shapes (1‑D shapes)
                        foreach (Aspose.Diagram.Shape shape in page.Shapes)
                        {
                            if (shape.OneD)
                            {
                                connectorCount++;
                            }
                        }

                        // Non‑connector shapes
                        int shapeCount = totalShapes - connectorCount;

                        // Output the summary for the current page
                        Console.WriteLine($"Page: {page.Name}, Shapes: {shapeCount}, Connectors: {connectorCount}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }