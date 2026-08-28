using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two Visio files to compare
                string firstDiagramPath = "Diagram1.vsdm";
                string secondDiagramPath = "Diagram2.vsdm";

                // Load the diagrams
                Diagram firstDiagram = new Diagram(firstDiagramPath);
                Diagram secondDiagram = new Diagram(secondDiagramPath);

                // Build dictionaries of module name -> code for each diagram
                var firstModules = GetVbaModules(firstDiagram);
                var secondModules = GetVbaModules(secondDiagram);

                // Report differences
                Console.WriteLine("=== VBA Modules Comparison Report ===");

                // Modules present only in the first diagram
                foreach (var kvp in firstModules)
                {
                    if (!secondModules.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"Module only in first diagram: {kvp.Key}");
                    }
                }

                // Modules present only in the second diagram
                foreach (var kvp in secondModules)
                {
                    if (!firstModules.ContainsKey(kvp.Key))
                    {
                        Console.WriteLine($"Module only in second diagram: {kvp.Key}");
                    }
                }

                // Modules with the same name but different code
                foreach (var kvp in firstModules)
                {
                    if (secondModules.TryGetValue(kvp.Key, out string secondCode))
                    {
                        string firstCode = kvp.Value;
                        if (!string.Equals(firstCode, secondCode, StringComparison.Ordinal))
                        {
                            Console.WriteLine($"Module '{kvp.Key}' differs between diagrams.");
                            Console.WriteLine("--- First Diagram Code ---");
                            Console.WriteLine(firstCode);
                            Console.WriteLine("--- Second Diagram Code ---");
                            Console.WriteLine(secondCode);
                            Console.WriteLine("---------------------------");
                        }
                    }
                }

                Console.WriteLine("=== End of Report ===");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Extracts VBA modules from a diagram into a dictionary (module name -> code)
        private static Dictionary<string, string> GetVbaModules(Diagram diagram)
        {
            var modules = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Ensure the diagram actually contains a VBA project
            if (diagram.VbaProject == null)
            {
                return modules;
            }

            // Iterate through all modules in the VBA project
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                // Use the module's name as the key and its source code as the value
                string name = module.Name ?? string.Empty;
                string code = module.Codes ?? string.Empty;
                if (!modules.ContainsKey(name))
                {
                    modules.Add(name, code);
                }
            }

            return modules;
        }
    }