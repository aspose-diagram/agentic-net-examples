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
            Console.Error.WriteLine("Please provide the path to a VSDX file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Output the shape ID (and page name for context)
                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}