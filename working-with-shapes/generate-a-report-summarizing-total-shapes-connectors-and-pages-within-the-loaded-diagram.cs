using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Count pages
            int totalPages = diagram.Pages.Count;

            // Initialize counters for shapes and connectors
            int totalShapes = 0;
            int totalConnectors = 0;

            // Iterate through each page and its shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    totalShapes++;

                    // Connectors are 1‑D shapes (dynamic connectors)
                    if (shape.OneD)
                    {
                        totalConnectors++;
                    }
                }
            }

            // Output the summary report
            Console.WriteLine($"Total Pages: {totalPages}");
            Console.WriteLine($"Total Shapes: {totalShapes}");
            Console.WriteLine($"Total Connectors: {totalConnectors}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
