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
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Access the VBA project; it may be null if the diagram has no VBA.
            VbaProject vbaProject = diagram.VbaProject;

            if (vbaProject == null)
            {
                Console.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Output basic VBA project information.
            Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
            Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");
            Console.WriteLine();

            // Regular expression to capture Sub and Function names.
            Regex macroRegex = new Regex(@"^\s*(Sub|Function)\s+([A-Za-z0-9_]+)", RegexOptions.IgnoreCase);

            // Iterate through each VBA module.
            foreach (VbaModule module in vbaProject.Modules)
            {
                Console.WriteLine($"Module: {module.Name}");

                string code = module.Codes ?? string.Empty;
                bool foundMacro = false;

                // Scan each line of the module's code for macro definitions.
                using (StringReader reader = new StringReader(code))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Match match = macroRegex.Match(line);
                        if (match.Success)
                        {
                            string macroName = match.Groups[2].Value;
                            Console.WriteLine($"  Macro: {macroName}");
                            foundMacro = true;
                        }
                    }
                }

                if (!foundMacro)
                {
                    Console.WriteLine("  (No macros found in this module)");
                }

                Console.WriteLine(); // Blank line between modules.
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}