using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page name (fallback to empty string if null)
                string pageName = page.Name ?? string.Empty;

                // Count total shapes on the page
                int shapeCount = page.Shapes.Count;

                // Count connector shapes (1-D shapes) on the page
                int connectorCount = 0;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD)
                    {
                        connectorCount++;
                    }
                }

                // Output the summary for the current page
                Console.WriteLine($"Page: {pageName}, Shapes: {shapeCount}, Connectors: {connectorCount}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}