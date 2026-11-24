using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Output basic VBA project information.
            Console.WriteLine($"VBA Project Name: {diagram.VbaProject.Name}");
            Console.WriteLine($"VBA Project Signed: {diagram.VbaProject.IsSigned}");

            bool anyErrors = false;

            // Iterate through all VBA modules.
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                Console.WriteLine($"\nAnalyzing Module: {module.Name}");

                string code = module.Codes ?? string.Empty;

                // Simple syntax checks:
                // 1. Ensure the module contains at least one Sub or Function.
                bool containsProcedure = code.Contains("Sub ") || code.Contains("Function ");

                // 2. Count Sub and End Sub occurrences.
                int subCount = CountOccurrences(code, "Sub ");
                int endSubCount = CountOccurrences(code, "End Sub");

                // 3. Count Function and End Function occurrences.
                int functionCount = CountOccurrences(code, "Function ");
                int endFunctionCount = CountOccurrences(code, "End Function");

                // Report missing procedure definitions.
                if (!containsProcedure)
                {
                    Console.Error.WriteLine($"Error: Module '{module.Name}' does not contain any Sub or Function definitions.");
                    anyErrors = true;
                }

                // Report mismatched Sub/End Sub counts.
                if (subCount != endSubCount)
                {
                    Console.Error.WriteLine($"Error: Module '{module.Name}' has mismatched Sub/End Sub count (Sub: {subCount}, End Sub: {endSubCount}).");
                    anyErrors = true;
                }

                // Report mismatched Function/End Function counts.
                if (functionCount != endFunctionCount)
                {
                    Console.Error.WriteLine($"Error: Module '{module.Name}' has mismatched Function/End Function count (Function: {functionCount}, End Function: {endFunctionCount}).");
                    anyErrors = true;
                }

                // If no issues were found for this module.
                if (containsProcedure && subCount == endSubCount && functionCount == endFunctionCount)
                {
                    Console.WriteLine($"Module '{module.Name}' passed syntax validation.");
                }
            }

            // Final summary.
            if (anyErrors)
            {
                Console.Error.WriteLine("\nVBA syntax validation completed with errors.");
            }
            else
            {
                Console.WriteLine("\nAll VBA modules passed syntax validation.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime exceptions.
            Console.Error.WriteLine($"Exception occurred: {ex.Message}");
        }
    }

    // Helper method to count non-overlapping occurrences of a substring.
    private static int CountOccurrences(string source, string substring)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(substring))
            return 0;

        int count = 0;
        int index = 0;
        while ((index = source.IndexOf(substring, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            count++;
            index += substring.Length;
        }
        return count;
    }
}