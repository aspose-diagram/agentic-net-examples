using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (must be a macro-enabled format, e.g., .vsdm)
            string filePath = "input.vsdm";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Ensure the diagram contains a VBA project
            if (diagram.VbaProject == null || diagram.VbaProject.Modules == null)
            {
                Console.WriteLine("No VBA project found in the diagram.");
                return;
            }

            // Regular expression to match Sub and Function declarations
            Regex macroRegex = new Regex(@"^\s*(Public|Private)?\s*(Sub|Function)\s+(\w+)", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            // Iterate through each VBA module
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                string moduleName = module.Name;
                string code = module.Codes ?? string.Empty;

                // Find all macro definitions in the module code
                MatchCollection matches = macroRegex.Matches(code);
                List<string> macroNames = new List<string>();

                foreach (Match match in matches)
                {
                    // The macro name is captured in group 3
                    if (match.Groups.Count > 3)
                    {
                        macroNames.Add(match.Groups[3].Value);
                    }
                }

                // Output the results
                if (macroNames.Count > 0)
                {
                    foreach (string macroName in macroNames)
                    {
                        Console.WriteLine($"Module: {moduleName}, Macro: {macroName}");
                    }
                }
                else
                {
                    Console.WriteLine($"Module: {moduleName} contains no macros.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
