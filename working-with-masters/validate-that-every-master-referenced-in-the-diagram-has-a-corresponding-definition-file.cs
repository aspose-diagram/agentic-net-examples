using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the diagram file path as the first argument.
        if (args.Length == 0)
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

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Collect all master names referenced by shapes.
                HashSet<string> referencedMasters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Iterate over pages using a for-loop (avoid foreach on diagram.Pages).
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate over shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape has an associated master, record its name.
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
                    // Use Masters.IsExist(string) to check presence.
                    if (!diagram.Masters.IsExist(masterName))
                    {
                        missingMasters.Add(masterName);
                    }
                }

                // Report results.
                if (missingMasters.Count == 0)
                {
                    Console.WriteLine("All referenced masters have corresponding definitions in the diagram.");
                }
                else
                {
                    Console.Error.WriteLine("The following masters are referenced but missing definition files:");
                    foreach (string missing in missingMasters)
                    {
                        Console.Error.WriteLine($"- {missing}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}