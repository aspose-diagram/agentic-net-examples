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
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path; if not provided, create one based on input name
        string outputPath;
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }
        else
        {
            string directory = Path.GetDirectoryName(inputPath) ?? "";
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(directory, $"{filenameWithoutExt}_withVba.vsdm");
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Output basic VBA project information to verify preservation
                if (diagram.VbaProject != null)
                {
                    Console.WriteLine($"VBA Project Name: {diagram.VbaProject.Name}");
                    Console.WriteLine($"Is Signed: {diagram.VbaProject.IsSigned}");
                    Console.WriteLine($"Modules Count: {diagram.VbaProject.Modules.Count}");

                    // Iterate through each VBA module and display its name and code length
                    foreach (VbaModule module in diagram.VbaProject.Modules)
                    {
                        int codeLength = module.Codes?.Length ?? 0;
                        Console.WriteLine($"Module: {module.Name}, Code Length: {codeLength}");
                    }
                }
                else
                {
                    Console.WriteLine("No VBA project found in the diagram.");
                }

                // Save the diagram using a macro-enabled format to preserve VBA
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}