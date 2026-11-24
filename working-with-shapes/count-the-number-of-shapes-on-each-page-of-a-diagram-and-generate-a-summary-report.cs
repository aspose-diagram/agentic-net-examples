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
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram using the required using pattern
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Manual page counter because Page does not expose an Index property
                int pageNumber = 1;

                // Iterate over each page in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Count the shapes on the current page
                    int shapeCount = page.Shapes.Count;

                    // Output the summary for this page
                    Console.WriteLine($"Page {pageNumber}: {shapeCount} shape(s)");

                    pageNumber++;
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}