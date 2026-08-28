using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Get the Visio file path from command‑line arguments or prompt the user
        string filePath;
        if (args.Length > 0)
        {
            filePath = args[0];
        }
        else
        {
            Console.Write("Enter Visio file path: ");
            filePath = Console.ReadLine();
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("File path is required.");
            return;
        }

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Verify that a VBA project with modules exists
        if (diagram.VbaProject == null || diagram.VbaProject.Modules == null)
        {
            Console.WriteLine("No VBA project or modules found in the diagram.");
            return;
        }

        int totalLines = 0;
        Console.WriteLine("VBA Module Line Count Report:");
        foreach (VbaModule module in diagram.VbaProject.Modules)
        {
            string code = module.Codes ?? string.Empty;
            int lineCount = 0;

            if (!string.IsNullOrEmpty(code))
            {
                // Split on both Windows (\r\n) and Unix (\n) line endings
                lineCount = code.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length;
            }

            Console.WriteLine($"- {module.Name}: {lineCount} lines");
            totalLines += lineCount;
        }

        Console.WriteLine($"Total lines across all modules: {totalLines}");
    }
}
