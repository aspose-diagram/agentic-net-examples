using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and keyword to search
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <diagramPath> <keyword>");
            return;
        }

        string diagramPath = args[0];
        string keyword = args[1];

        // Load the Visio diagram
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

        // Verify that a VBA project with modules exists
        if (diagram.VbaProject == null || diagram.VbaProject.Modules == null || diagram.VbaProject.Modules.Count == 0)
        {
            Console.WriteLine("No VBA project or modules found in the diagram.");
            return;
        }

        bool found = false;

        // Iterate through each VBA module and search for the keyword
        foreach (VbaModule module in diagram.VbaProject.Modules)
        {
            if (!string.IsNullOrEmpty(module.Codes) &&
                module.Codes.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Console.WriteLine($"Keyword \"{keyword}\" found in module: {module.Name}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine($"Keyword \"{keyword}\" not found in any VBA module.");
        }
    }
}
