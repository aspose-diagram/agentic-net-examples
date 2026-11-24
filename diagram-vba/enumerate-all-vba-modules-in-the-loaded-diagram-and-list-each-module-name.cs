using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Verify that an input file path was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Check whether the diagram contains a VBA project.
                if (diagram.VbaProject == null)
                {
                    Console.WriteLine("No VBA project found in the diagram.");
                    return;
                }

                // Access the collection of VBA modules.
                VbaModuleCollection modules = diagram.VbaProject.Modules;

                // If there are no modules, inform the user.
                if (modules == null || modules.Count == 0)
                {
                    Console.WriteLine("No VBA modules found.");
                }
                else
                {
                    Console.WriteLine("VBA Modules:");
                    // Enumerate each module and output its name.
                    foreach (VbaModule module in modules)
                    {
                        Console.WriteLine($"- {module.Name}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}