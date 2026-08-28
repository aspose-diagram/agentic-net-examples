using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaKeywordSearcher
{
    // Searches all VBA modules in a Visio diagram for a given keyword.
    // Returns the names of modules where the keyword is found.
    public static List<string> FindModulesWithKeyword(string diagramPath, string keyword)
    {
        // Load the Visio diagram (lifecycle rule: use Diagram constructor for loading)
        Diagram diagram = new Diagram(diagramPath);

        // Access the VBA project contained in the diagram
        VbaProject vbaProject = diagram.VbaProject;

        List<string> matchingModules = new List<string>();

        // Iterate through each VBA module in the project
        foreach (VbaModule module in vbaProject.Modules)
        {
            // Ensure the module has code and check for the keyword (case‑insensitive)
            if (!string.IsNullOrEmpty(module.Codes) &&
                module.Codes.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                matchingModules.Add(module.Name);
            }
        }

        return matchingModules;
    }

    static void Main()
    {
        try
        {

            // Example usage
            string diagramFile = "sample.vsdx";   // Path to the Visio file
            string searchKeyword = "MyFunction"; // Keyword to look for in VBA code

            List<string> modules = FindModulesWithKeyword(diagramFile, searchKeyword);

            Console.WriteLine($"Modules containing \"{searchKeyword}\":");
            foreach (string moduleName in modules)
            {
                Console.WriteLine(moduleName);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
