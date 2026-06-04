using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaValidator
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file containing VBA code
            string filePath = "input.vsdx";

            // Load the diagram (using Aspose.Diagram's load functionality)
            Diagram diagram = new Diagram(filePath);

            // Access the VBA project embedded in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Iterate through each VBA module in the project
            foreach (VbaModule module in vbaProject.Modules)
            {
                Console.WriteLine($"Module Name: {module.Name}");
                Console.WriteLine("Module Code:");
                Console.WriteLine(module.Codes);
                Console.WriteLine(new string('-', 40));

                // Basic syntax validation (placeholder for real compilation check)
                try
                {
                    ValidateVbaCode(module.Codes);
                    Console.WriteLine("No syntax errors detected.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Syntax error: {ex.Message}");
                }

                Console.WriteLine();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Very simple VBA syntax validator – checks matching Sub/End Sub and Function/End Function pairs
    static void ValidateVbaCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return;

        int subCount = CountOccurrences(code, "Sub ");
        int endSubCount = CountOccurrences(code, "End Sub");
        int funcCount = CountOccurrences(code, "Function ");
        int endFuncCount = CountOccurrences(code, "End Function");

        if (subCount != endSubCount)
            throw new Exception("Mismatched Sub/End Sub statements.");

        if (funcCount != endFuncCount)
            throw new Exception("Mismatched Function/End Function statements.");
    }

    // Helper to count case‑insensitive occurrences of a substring
    static int CountOccurrences(string source, string value)
    {
        int count = 0;
        int index = 0;
        while ((index = source.IndexOf(value, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            count++;
            index += value.Length;
        }
        return count;
    }
}
