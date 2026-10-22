using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // List to hold IDs of shapes that use the "Process" master
        List<long> processShapeIds = new List<long>();

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has an associated master before accessing its name
                        if (shape.Master != null && shape.Master.Name == "Process")
                        {
                            // Collect the shape's ID for analysis
                            processShapeIds.Add(shape.ID);
                        }
                    }
                }
            }

            // Output the collected IDs
            Console.WriteLine($"Found {processShapeIds.Count} shape(s) with master name \"Process\".");
            foreach (long id in processShapeIds)
            {
                Console.WriteLine($"Shape ID: {id}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}