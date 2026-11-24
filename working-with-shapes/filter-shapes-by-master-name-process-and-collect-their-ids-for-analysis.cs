using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to hold IDs of shapes whose master name is "Process".
                List<long> processShapeIds = new List<long>();

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has an associated master.
                        if (shape.Master == null)
                            continue;

                        // Compare the master name with the target name "Process".
                        if (shape.Master.Name == "Process")
                        {
                            // Collect the shape ID for analysis.
                            processShapeIds.Add(shape.ID);
                        }
                    }
                }

                // Output the collected IDs.
                Console.WriteLine("Shape IDs with master name \"Process\":");
                foreach (long id in processShapeIds)
                {
                    Console.WriteLine(id);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}