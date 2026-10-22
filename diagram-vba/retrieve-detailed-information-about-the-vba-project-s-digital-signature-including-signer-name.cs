using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Access the VBA project
            VbaProject vbaProject = diagram.VbaProject;

            // Output basic VBA project information
            Console.WriteLine("VBA Project Information:");
            Console.WriteLine($"- Project Name : {vbaProject.Name}");
            Console.WriteLine($"- Is Signed    : {vbaProject.IsSigned}");

            // List all VBA modules
            Console.WriteLine("\nModules:");
            if (vbaProject.Modules != null && vbaProject.Modules.Count > 0)
            {
                foreach (VbaModule module in vbaProject.Modules)
                {
                    Console.WriteLine($"  * Module Name : {module.Name}");
                    // Show a preview of the code (first 100 characters) if available
                    if (!string.IsNullOrEmpty(module.Codes))
                    {
                        string preview = module.Codes.Length > 100 ? module.Codes.Substring(0, 100) + "..." : module.Codes;
                        Console.WriteLine($"    Code Preview: {preview}");
                    }
                    else
                    {
                        Console.WriteLine("    (No code)");
                    }
                }
            }
            else
            {
                Console.WriteLine("  (No modules found)");
            }

            // Note about detailed signer information
            Console.WriteLine("\nNote: Detailed signer information (e.g., signer name, certificate details) is not exposed by the Aspose.Diagram VbaProject API. Only the IsSigned flag is available.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}