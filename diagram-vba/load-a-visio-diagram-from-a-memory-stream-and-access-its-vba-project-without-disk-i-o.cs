using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Read the Visio file into a memory stream (no disk I/O after this point)
            byte[] fileBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                // Load the diagram from the memory stream
                using (Diagram diagram = new Diagram(memoryStream))
                {
                    // Access the VBA project (read‑only)
                    VbaProject vbaProject = diagram.VbaProject;

                    if (vbaProject == null)
                    {
                        Console.WriteLine("The diagram does not contain a VBA project.");
                        return;
                    }

                    // Output basic VBA project information
                    Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
                    Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");

                    // List all VBA modules and their code
                    Console.WriteLine("VBA Modules:");
                    foreach (VbaModule module in vbaProject.Modules)
                    {
                        Console.WriteLine($"- Module Name: {module.Name}");
                        Console.WriteLine("  Code:");
                        Console.WriteLine(module.Codes ?? string.Empty);
                        Console.WriteLine(new string('-', 40));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}