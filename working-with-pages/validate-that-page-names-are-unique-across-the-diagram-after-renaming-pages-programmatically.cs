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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Rename each page (example: prepend "Renamed_" to the original name)
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                page.Name = "Renamed_" + page.Name;
                // Also update the universal name to keep consistency
                page.NameU = page.Name;
            }

            // Validate that all page names are unique after renaming
            HashSet<string> pageNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            bool duplicateFound = false;

            foreach (Page page in diagram.Pages)
            {
                if (!pageNames.Add(page.Name))
                {
                    Console.WriteLine($"Duplicate page name detected: {page.Name}");
                    duplicateFound = true;
                }
            }

            if (duplicateFound)
            {
                Console.WriteLine("Page name validation failed: duplicates exist.");
            }
            else
            {
                Console.WriteLine("All page names are unique.");
            }

            // Save the modified diagram
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                // Ensure each Visio page is saved as a separate PDF page
                SplitMultiPages = true
            };
            diagram.Save("output.pdf", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
