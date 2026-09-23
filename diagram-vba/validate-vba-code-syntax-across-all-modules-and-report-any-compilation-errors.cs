using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: dotnet run <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Load the diagram (no password handling required)
        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Access the VBA project
        VbaProject vbaProject = diagram.VbaProject;
        if (vbaProject == null)
        {
            Console.WriteLine("No VBA project found in the diagram.");
            return;
        }

        bool anyErrors = false;

        // Iterate through all VBA modules
        for (int i = 0; i < vbaProject.Modules.Count; i++)
        {
            VbaModule module = vbaProject.Modules[i];
            string code = module.Codes ?? string.Empty;

            // Simple syntax validation: ensure each Sub has a matching End Sub
            // This is a rudimentary check and may not catch all errors.
            int subCount = CountOccurrences(code, "Sub ");
            int endSubCount = CountOccurrences(code, "End Sub");

            if (subCount != endSubCount)
            {
                anyErrors = true;
                Console.WriteLine($"Module '{module.Name}' (Index {i}) has mismatched Sub/End Sub statements.");
                Console.WriteLine($"  Sub statements: {subCount}, End Sub statements: {endSubCount}");
            }

            // Additional basic check: ensure the module contains at least one Sub or Function
            if (!code.Contains("Sub ") && !code.Contains("Function "))
            {
                anyErrors = true;
                Console.WriteLine($"Module '{module.Name}' (Index {i}) does not contain any Sub or Function definitions.");
            }
        }

        if (!anyErrors)
        {
            Console.WriteLine("All VBA modules passed the basic syntax validation.");
        }
    }

    // Helper method to count non-overlapping occurrences of a substring
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
