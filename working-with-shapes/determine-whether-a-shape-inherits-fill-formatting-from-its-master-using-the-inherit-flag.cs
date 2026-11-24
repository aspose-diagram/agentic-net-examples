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
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a master; otherwise inheritance from master is not applicable
                    if (shape.Master == null)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' has no master. Inheritance not applicable.");
                        continue;
                    }

                    // Retrieve the current fill foreground color
                    string fillForegnd = shape.Fill.FillForegnd.Value;

                    // Retrieve the inherited fill foreground color (from master/style)
                    string inheritForegnd = shape.InheritFill.FillForegnd.Value;

                    // Determine if the shape is inheriting the fill foreground color from its master
                    bool isInheritingFill = string.Equals(fillForegnd, inheritForegnd, StringComparison.OrdinalIgnoreCase);

                    // Output the result
                    Console.WriteLine($"Shape ID {shape.ID} (Master: {shape.Master.Name}) inherits fill: {isInheritingFill}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}