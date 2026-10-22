using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Output basic VBA project information.
                VbaProject vba = diagram.VbaProject;
                if (vba != null)
                {
                    Console.WriteLine($"VBA Project Name: {vba.Name}");
                    Console.WriteLine($"VBA Project IsSigned: {vba.IsSigned}");

                    // List all VBA modules.
                    Console.WriteLine("VBA Modules:");
                    foreach (VbaModule module in vba.Modules)
                    {
                        Console.WriteLine($"  Module Name: {module.Name}");
                        // Optionally display a snippet of the code.
                        string codeSnippet = module.Codes?.Length > 100
                            ? module.Codes.Substring(0, 100) + "..."
                            : module.Codes;
                        Console.WriteLine($"    Code: {codeSnippet}");
                    }
                }
                else
                {
                    Console.WriteLine("No VBA project found in the diagram.");
                }

                // Save the diagram preserving the VBA project.
                // Use the macro‑enabled VSDM format.
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\" preserving VBA.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}