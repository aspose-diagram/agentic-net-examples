using System.IO;
using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Get the VBA project from the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // If there is no VBA project, exit
            if (vbaProject == null)
            {
                Console.WriteLine("No VBA project found in the diagram.");
                return;
            }

            // Regular expression to match Sub or Function definitions (ignores case and leading spaces)
            Regex macroRegex = new Regex(@"^\s*(Sub|Function)\s+([A-Za-z_][A-Za-z0-9_]*)", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            // Iterate through all modules in the VBA project
            foreach (VbaModule module in vbaProject.Modules)
            {
                string moduleName = module.Name;
                string code = module.Codes ?? string.Empty;

                // Find all macro definitions in the module code
                MatchCollection matches = macroRegex.Matches(code);

                // If no macros are found, continue to next module
                if (matches.Count == 0)
                    continue;

                Console.WriteLine($"Module: {moduleName}");
                foreach (Match match in matches)
                {
                    // match.Groups[2] contains the macro name
                    string macroName = match.Groups[2].Value;
                    Console.WriteLine($"  Macro: {macroName}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
