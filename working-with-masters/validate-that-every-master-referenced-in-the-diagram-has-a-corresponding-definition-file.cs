using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <diagramFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Collect all master names referenced by shapes in the diagram.
                HashSet<string> referencedMasters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Iterate through each page.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape is based on a master, record its name.
                        if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                        {
                            referencedMasters.Add(shape.Master.Name);
                        }
                    }
                }

                // Validate that each referenced master exists in the diagram's master collection.
                List<string> missingMasters = new List<string>();
                foreach (string masterName in referencedMasters)
                {
                    // Use the IsExist(string) method to check presence.
                    if (!diagram.Masters.IsExist(masterName))
                    {
                        missingMasters.Add(masterName);
                    }
                }

                // Output the validation results.
                if (missingMasters.Count == 0)
                {
                    Console.WriteLine("All referenced masters have corresponding definitions.");
                }
                else
                {
                    Console.Error.WriteLine("The following masters are referenced but missing definitions:");
                    foreach (string missing in missingMasters)
                    {
                        Console.Error.WriteLine($"- {missing}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}