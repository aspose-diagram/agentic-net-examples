using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaKeywordSearcher
{
    // Searches all VBA modules in a Visio diagram for a given keyword.
    // Returns a list of module names where the keyword is found.
    public static List<string> FindKeywordInVbaModules(string diagramPath, string keyword)
    {
        // Load the Visio diagram (uses the provided load rule)
        Diagram diagram = new Diagram(diagramPath);

        var result = new List<string>();

        // Ensure the diagram contains a VBA project
        VbaProject vbaProject = diagram.VbaProject;
        if (vbaProject == null)
            return result; // No VBA project present

        // Iterate through all VBA modules
        foreach (VbaModule module in vbaProject.Modules)
        {
            // Guard against null code
            string code = module.Codes ?? string.Empty;

            // Perform case‑insensitive search for the keyword
            if (code.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Add the module name to the result list
                result.Add(module.Name);
            }
        }

        return result;
    }

    // Example usage
    static void Main()
    {
        try
        {

            string diagramFile = @"C:\Diagrams\Sample.vsdx";
            string searchKeyword = "MyFunction";

            List<string> modulesWithKeyword = FindKeywordInVbaModules(diagramFile, searchKeyword);

            Console.WriteLine($"Modules containing \"{searchKeyword}\":");
            foreach (string moduleName in modulesWithKeyword)
            {
                Console.WriteLine($"- {moduleName}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
