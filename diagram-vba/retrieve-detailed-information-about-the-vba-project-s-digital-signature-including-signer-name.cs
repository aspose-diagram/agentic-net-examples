using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists
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
            Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
            Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");

            // List all VBA modules and basic details
            Console.WriteLine("VBA Modules:");
            foreach (VbaModule module in vbaProject.Modules)
            {
                Console.WriteLine($"  Module Name: {module.Name}");
                int codeLength = module.Codes != null ? module.Codes.Length : 0;
                Console.WriteLine($"    Code Length: {codeLength} characters");
            }

            // Provide information about signature details
            if (vbaProject.IsSigned)
            {
                Console.WriteLine("Detailed signer information (e.g., signer name) is not exposed by the Aspose.Diagram API.");
                Console.WriteLine("To obtain signer details, use external certificate APIs or libraries.");
            }
            else
            {
                Console.WriteLine("The VBA project is not digitally signed.");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}