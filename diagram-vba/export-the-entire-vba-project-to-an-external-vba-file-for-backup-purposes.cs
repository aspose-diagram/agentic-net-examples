using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVbaPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project; it may be null if no macros are present.
            var vbaProject = diagram.VbaProject;
            if (vbaProject == null)
            {
                Console.Error.WriteLine("No VBA project found in the diagram.");
                return;
            }

            // Open a StreamWriter to create the backup .vba file.
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Write a header for the exported VBA project.
                writer.WriteLine("' Exported VBA Project");
                writer.WriteLine($"' Source Diagram: {Path.GetFileName(inputPath)}");
                writer.WriteLine();

                // Iterate through each VBA module and write its code.
                foreach (VbaModule module in vbaProject.Modules)
                {
                    writer.WriteLine($"' Module: {module.Name}");
                    writer.WriteLine(module.Codes);
                    writer.WriteLine(); // Separate modules with an empty line.
                }
            }

            Console.WriteLine($"VBA project exported successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}