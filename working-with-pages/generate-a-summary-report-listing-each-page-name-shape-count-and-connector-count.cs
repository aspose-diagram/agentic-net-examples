using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (lifecycle: load)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page name
                string pageName = page.Name;

                // Count shapes on the page
                int shapeCount = page.Shapes.Count;

                // Count connectors (connections) on the page
                int connectorCount = page.Connects.Count;

                // Output the summary for the current page
                Console.WriteLine($"Page: {pageName}, Shapes: {shapeCount}, Connectors: {connectorCount}");
            }

            // No saving required for the summary report

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
