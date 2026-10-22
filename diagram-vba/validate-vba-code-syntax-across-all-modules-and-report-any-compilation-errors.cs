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

            // Check if the diagram contains a VBA project.
            if (diagram.VbaProject == null)
            {
                Console.WriteLine("No VBA project found in the diagram.");
                return;
            }

            bool anyErrors = false;

            // Iterate through each VBA module.
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                Console.WriteLine($"Analyzing module: {module.Name}");

                string code = module.Codes ?? string.Empty;

                // Count occurrences of Sub, End Sub, and Function (case-insensitive).
                int subCount = CountOccurrences(code, "Sub ");
                int endSubCount = CountOccurrences(code, "End Sub");
                int functionCount = CountOccurrences(code, "Function ");

                // Validate Sub/End Sub pairing.
                if (subCount != endSubCount)
                {
                    anyErrors = true;
                    Console.Error.WriteLine($"[Error] Module '{module.Name}': Mismatched Sub/End Sub count (Sub: {subCount}, End Sub: {endSubCount}).");
                }

                // Ensure at least one Sub or Function exists.
                if (subCount == 0 && functionCount == 0)
                {
                    anyErrors = true;
                    Console.Error.WriteLine($"[Error] Module '{module.Name}': No Sub or Function definitions found.");
                }
            }

            if (!anyErrors)
            {
                Console.WriteLine("VBA syntax validation completed successfully. No errors detected.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime exceptions.
            Console.Error.WriteLine($"Exception occurred: {ex.Message}");
        }
    }

    // Helper method to count non-overlapping occurrences of a substring (case-insensitive).
    private static int CountOccurrences(string source, string value)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(value))
            return 0;

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