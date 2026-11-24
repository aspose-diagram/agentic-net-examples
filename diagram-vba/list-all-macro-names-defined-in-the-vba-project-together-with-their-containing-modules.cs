using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Access the VBA project; it may be null if the diagram has no macros.
            VbaProject vbaProject = diagram.VbaProject;
            if (vbaProject == null)
            {
                Console.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Iterate through each VBA module in the project.
            foreach (VbaModule module in vbaProject.Modules)
            {
                Console.WriteLine($"Module: {module.Name}");

                // Extract macro names (Sub and Function) from the module's code.
                var macroNames = ExtractMacroNames(module.Codes);

                if (macroNames.Count == 0)
                {
                    Console.WriteLine("  (No macros found in this module)");
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
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses VBA code and returns a list of macro names (Sub and Function).
    /// </summary>
    /// <param name="code">The VBA source code of a module.</param>
    /// <returns>A list of macro names.</returns>
    private static System.Collections.Generic.List<string> ExtractMacroNames(string code)
    {
        var macros = new System.Collections.Generic.List<string>();

        if (string.IsNullOrEmpty(code))
            return macros;

        // Use regular expressions to find Sub and Function declarations.
        // Pattern matches lines like: Sub MacroName( ... ) or Function MacroName( ... )
        string pattern = @"^\s*(Sub|Function)\s+([A-Za-z_][A-Za-z0-9_]*)\s*\(";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

        foreach (Match match in regex.Matches(code))
        {
            if (match.Groups.Count >= 3)
            {
                string macroName = match.Groups[2].Value;
                macros.Add(macroName);
            }
        }

        return macros;
    }
}