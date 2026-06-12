using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Count pages
            int pageCount = diagram.Pages.Count;

            // Initialize counters for shapes and connectors
            int totalShapes = 0;
            int totalConnectors = 0;

            // Iterate through each page and each shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    totalShapes++;

                    // Connectors are 1‑D shapes (OneD == true)
                    if (shape.OneD)
                    {
                        totalConnectors++;
                    }
                }
            }

            // Output the summary report
            Console.WriteLine($"Pages: {pageCount}");
            Console.WriteLine($"Total Shapes: {totalShapes}");
            Console.WriteLine($"Total Connectors: {totalConnectors}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
