using System.IO;
using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class ListVbaMacros
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (replace with your actual file)
            string diagramPath = "input.vsdx";

            // Load the Visio diagram using Aspose.Diagram (lifecycle rule)
            Diagram diagram = new Diagram(diagramPath);

            // Get the VBA project from the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // If there is no VBA project, exit
            if (vbaProject == null)
            {
                Console.WriteLine("No VBA project found in the diagram.");
                return;
            }

            // Iterate through all VBA modules
            foreach (VbaModule module in vbaProject.Modules)
            {
                string moduleName = module.Name;
                string code = module.Codes ?? string.Empty;

                // Use a simple regex to find Sub and Function declarations
                // This captures names after "Sub" or "Function" ignoring case and optional whitespace
                Regex macroRegex = new Regex(@"\b(Sub|Function)\s+([A-Za-z_][A-Za-z0-9_]*)", RegexOptions.IgnoreCase);
                MatchCollection matches = macroRegex.Matches(code);

                // If no macros found, continue to next module
                if (matches.Count == 0)
                    continue;

                Console.WriteLine($"Module: {moduleName}");
                foreach (Match match in matches)
                {
                    // Group 2 contains the macro name
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
