using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Verify that the diagram contains at least one page.
            if (diagram.Pages == null || diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Iterate through pages and shapes to log master names.
            foreach (Page page in diagram.Pages)
            {
                // Verify that the page has shapes.
                if (page.Shapes == null || page.Shapes.Count == 0)
                {
                    Console.WriteLine($"Page {page.Name} contains no shapes.");
                    continue;
                }

                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the master name if the shape is based on a master.
                    if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' uses master: {shape.Master.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' does not have an associated master.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}