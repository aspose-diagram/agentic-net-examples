using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect the first argument to be the path of the Visio file to validate.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: MasterValidationApp <diagram-file-path>");
                return;
            }

            string diagramPath = args[0];

            // Load the diagram.
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Collect all master names referenced by shapes.
            var referencedMasters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Only consider shapes that have an associated master.
                    if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                    {
                        referencedMasters.Add(shape.Master.Name);
                    }
                }
            }

            // Validate that each referenced master exists in the diagram's master collection.
            bool allMastersPresent = true;
            foreach (string masterName in referencedMasters)
            {
                // Use the IsExist method that accepts a master name.
                if (!diagram.Masters.IsExist(masterName))
                {
                    Console.WriteLine($"Missing master definition: \"{masterName}\"");
                    allMastersPresent = false;
                }
            }

            if (allMastersPresent)
            {
                Console.WriteLine("All referenced masters have corresponding definitions.");
            }
            else
            {
                Console.WriteLine("One or more masters are missing definitions.");
            }

            // No need to save the diagram; just dispose.
            diagram.Dispose();
        }
    }