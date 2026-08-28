using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Example: rename each page programmatically
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                page.Name = $"Page_{i}";
            }

            // Validate that all page names are unique
            HashSet<string> pageNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            bool duplicateFound = false;

            foreach (Page page in diagram.Pages)
            {
                if (!pageNames.Add(page.Name))
                {
                    duplicateFound = true;
                    Console.WriteLine($"Duplicate page name detected: {page.Name}");
                }
            }

            if (!duplicateFound)
            {
                Console.WriteLine("All page names are unique.");
            }

            // Save the diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
