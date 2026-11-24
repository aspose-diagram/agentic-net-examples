using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to an existing Visio file used for the test
        string diagramPath = "sample.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard to ensure the input file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.Del == BOOL.False)
                {
                    targetShape = s;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No non‑deleted shape found on the first page.");
                return;
            }

            // Record the connection count before adding a new connection point
            int countBefore = targetShape.Connections.Count;

            // Create a new connection point and add it to the shape
            Connection newConnection = new Connection();
            targetShape.Connections.Add(newConnection);

            // Record the connection count after the addition
            int countAfter = targetShape.Connections.Count;

            // Verify that the count increased by exactly one
            if (countAfter != countBefore + 1)
            {
                throw new Exception($"Connection count mismatch. Expected {countBefore + 1}, but got {countAfter}.");
            }

            Console.WriteLine("Test passed: Adding a connection point increased the Connections count by one.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}