using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <path-to-vsdx-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the specified file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Output the page name and shape ID
                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or enumeration to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}