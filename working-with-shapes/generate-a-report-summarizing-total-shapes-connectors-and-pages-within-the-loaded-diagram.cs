using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file. Use first command‑line argument if supplied.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                int pageCount = 0;
                int shapeCount = 0;
                int connectorCount = 0;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    pageCount++;

                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        shapeCount++;

                        // Connectors are 1‑D shapes.
                        if (shape.OneD)
                        {
                            connectorCount++;
                        }
                    }
                }

                // Output the summary report.
                Console.WriteLine($"Total pages: {pageCount}");
                Console.WriteLine($"Total shapes: {shapeCount}");
                Console.WriteLine($"Total connectors: {connectorCount}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }