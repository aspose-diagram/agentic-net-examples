using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // Keyword to search for inside VBA modules
            string keyword = "YourKeyword";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the VBA project contained in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Collection to store names of modules where the keyword is found
            List<string> matchingModules = new List<string>();

            // Iterate through each VBA module in the project
            foreach (VbaModule module in vbaProject.Modules)
            {
                // Retrieve the VBA code of the current module
                string code = module.Codes;

                // Check if the code contains the keyword (case‑insensitive)
                if (!string.IsNullOrEmpty(code) && code.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    // Store module type and name as location information
                    matchingModules.Add($"{module.Type}:{module.Name}");
                }
            }

            // Output the locations of modules that contain the keyword
            Console.WriteLine("Modules containing the keyword:");
            foreach (string location in matchingModules)
            {
                Console.WriteLine(location);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
