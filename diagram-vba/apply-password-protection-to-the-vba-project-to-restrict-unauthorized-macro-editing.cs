using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project (read‑only property).
            VbaProject vbaProject = diagram.VbaProject;

            if (vbaProject == null)
            {
                Console.WriteLine("The diagram does not contain a VBA project.");
            }
            else
            {
                // Output basic VBA project information.
                Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
                Console.WriteLine($"VBA Project IsSigned: {vbaProject.IsSigned}");

                // List existing VBA modules.
                Console.WriteLine("Existing VBA Modules:");
                foreach (VbaModule module in vbaProject.Modules)
                {
                    Console.WriteLine($"- {module.Name}");
                }

                // TODO: Apply password protection to the VBA project when a supported API becomes available.
                // Aspose.Diagram currently does not expose a property or method to set a VBA project password.
                // When such an API is added, it should be invoked here, e.g.:
                // vbaProject.Password = "YourPassword";
            }

            // Save the diagram in a macro‑enabled format (VSDM) to preserve VBA content.
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}