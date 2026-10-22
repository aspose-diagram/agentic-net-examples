using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio file into a memory stream (no further disk I/O).
            byte[] fileBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                // Create a Diagram instance from the memory stream.
                Diagram diagram = new Diagram(memoryStream);

                // Access the VBA project (read‑only).
                VbaProject vbaProject = diagram.VbaProject;

                if (vbaProject == null)
                {
                    Console.WriteLine("The diagram does not contain a VBA project.");
                }
                else
                {
                    // Output basic VBA project information.
                    Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
                    Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");

                    // List all VBA modules and their source code.
                    foreach (VbaModule module in vbaProject.Modules)
                    {
                        Console.WriteLine($"--- Module: {module.Name} ---");
                        Console.WriteLine(module.Codes);
                        Console.WriteLine();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}