using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class DiagramValidator
{
    static void Main(string[] args)
    {
        try
        {

            // Load the diagram file (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // HashSet to store encountered page names
            HashSet<string> pageNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                string name = page.Name;

                // Check for duplicate page name
                if (!pageNames.Add(name))
                {
                    // Duplicate found – raise an exception with details
                    throw new InvalidOperationException($"Duplicate page name detected: \"{name}\".");
                }
            }

            // If execution reaches here, all page names are unique
            Console.WriteLine("All page names are unique.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
