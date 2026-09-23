using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the diagram file path as the first argument.
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: MasterValidation <diagram-file-path>");
                    return;
                }

                string diagramPath = args[0];

                // Load the Visio diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Collect all master names referenced by shapes in the diagram.
                var referencedMasters = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);

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

                // Verify that each referenced master exists in the diagram's master collection.
                int missingCount = 0;
                foreach (string masterName in referencedMasters)
                {
                    // Diagram.Masters.IsExist(string) checks for the presence of a master by name.
                    if (!diagram.Masters.IsExist(masterName))
                    {
                        Console.WriteLine($"Missing master definition: {masterName}");
                        missingCount++;
                    }
                }

                if (missingCount == 0)
                {
                    Console.WriteLine("All masters referenced in the diagram have corresponding definitions.");
                }
                else
                {
                    Console.WriteLine($"Total missing masters: {missingCount}");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }