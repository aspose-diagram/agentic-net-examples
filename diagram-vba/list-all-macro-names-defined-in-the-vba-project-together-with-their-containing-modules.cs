using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class ListVbaMacros
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file that contains the VBA project
            string inputPath = "input.vsdx";

            // Load the diagram (use the provided load rule)
            Diagram diagram = new Diagram(inputPath);

            // Get the VBA project from the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Iterate through all modules in the VBA project
            foreach (VbaModule module in vbaProject.Modules)
            {
                // Retrieve the VBA source code of the current module
                string code = module.Codes ?? string.Empty;

                // Extract macro (Sub/Function) names using a simple regex
                List<string> macroNames = ExtractMacroNames(code);

                // Output the module name and its macros
                Console.WriteLine($"Module: {module.Name}");
                if (macroNames.Count == 0)
                {
                    Console.WriteLine("  (No macros found)");
                }
                else
                {
                    foreach (string macro in macroNames)
                    {
                        Console.WriteLine($"  Macro: {macro}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method that parses VBA code and returns a list of macro names
    private static List<string> ExtractMacroNames(string code)
    {
        var macros = new List<string>();

        // Pattern matches lines that start a Sub or Function definition.
        // It captures the macro name in group 3.
        string pattern = @"^\s*(Public\s+|Private\s+)?(Sub|Function)\s+(\w+)";
        var regex = new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);

        foreach (Match match in regex.Matches(code))
        {
            // Group 3 contains the macro name
            string macroName = match.Groups[3].Value;
            if (!string.IsNullOrEmpty(macroName))
            {
                macros.Add(macroName);
            }
        }

        return macros;
    }
}
