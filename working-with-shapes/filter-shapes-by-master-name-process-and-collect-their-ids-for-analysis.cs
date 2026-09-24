using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: program <inputVisioFile>");
            return;
        }

        // Assign the first argument to a variable representing the input file
        string inputPath = args[0];

        // Guard: ensure the specified file actually exists on disk
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Prepare a list to hold the IDs of shapes whose master name is "Process"
        List<long> processShapeIds = new List<long>();

        try
        {
            // Load the Visio diagram from the provided file path
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check that the shape has an associated master and that its name matches "Process"
                    if (shape.Master != null && shape.Master.Name == "Process")
                    {
                        // Store the shape's unique identifier for later analysis
                        processShapeIds.Add(shape.ID);
                    }
                }
            }

            // Output the collected shape IDs to the console
            Console.WriteLine("Shape IDs with master name 'Process':");
            foreach (long id in processShapeIds)
            {
                Console.WriteLine(id);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during loading or processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}