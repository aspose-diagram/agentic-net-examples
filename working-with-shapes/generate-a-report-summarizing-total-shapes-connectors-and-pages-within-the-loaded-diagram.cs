using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file to be loaded.
                string inputPath = "input.vsdx";

                // Load the diagram from the specified file.
                Diagram diagram = new Diagram(inputPath);

                // Initialize counters.
                int totalPages = diagram.Pages.Count;
                int totalShapes = 0;
                int totalConnectors = 0;

                // Iterate through each page explicitly typed.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Count all shapes on the current page.
                    totalShapes += page.Shapes.Count;

                    // Iterate through each shape to identify connectors (1‑D shapes).
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        if (shape.OneD) // Connectors are 1‑D shapes.
                        {
                            totalConnectors++;
                        }
                    }
                }

                // Output the summary report.
                Console.WriteLine("Diagram Summary Report");
                Console.WriteLine("----------------------");
                Console.WriteLine($"Total Pages    : {totalPages}");
                Console.WriteLine($"Total Shapes   : {totalShapes}");
                Console.WriteLine($"Total Connectors: {totalConnectors}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }