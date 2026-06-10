using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Determine the input file path.
                string inputPath;
                if (args.Length > 0)
                {
                    inputPath = args[0];
                }
                else
                {
                    Console.Write("Enter the path to the Visio diagram file: ");
                    inputPath = Console.ReadLine();
                }

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Validate that each page has a unique name.
                var pageNames = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Page page in diagram.Pages)
                {
                    // Use the universal name (NameU) for consistency.
                    string name = page.NameU ?? string.Empty;

                    if (pageNames.Contains(name))
                    {
                        // Duplicate found – raise an exception with details.
                        throw new Exception($"Duplicate page name detected: \"{name}\".");
                    }

                    pageNames.Add(name);
                }

                Console.WriteLine("All page names are unique.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }