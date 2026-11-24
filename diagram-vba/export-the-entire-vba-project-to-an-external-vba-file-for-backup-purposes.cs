using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output VBA backup file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVbaPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the VBA project (read‑only)
                VbaProject vbaProject = diagram.VbaProject;

                // Prepare a StringBuilder to collect all VBA modules
                StringBuilder sb = new StringBuilder();

                // Write project metadata
                sb.AppendLine($"' VBA Project Name: {vbaProject.Name}");
                sb.AppendLine($"' Is Signed: {vbaProject.IsSigned}");
                sb.AppendLine();

                // Iterate through each VBA module and append its code
                foreach (VbaModule module in vbaProject.Modules)
                {
                    sb.AppendLine($"' Module: {module.Name}");
                    sb.AppendLine(module.Codes ?? string.Empty);
                    sb.AppendLine();
                }

                // Write the collected VBA code to the specified .vba file
                File.WriteAllText(outputPath, sb.ToString());

                Console.WriteLine($"VBA project exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}