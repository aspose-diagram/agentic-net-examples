using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static async Task Main(string[] args)
    {
        // Input diagram file path (modify as needed)
        string inputPath = "input.vsdm";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Asynchronously load the diagram
            using (Diagram diagram = await LoadDiagramAsync(inputPath))
            {
                // Retrieve the VBA project from the loaded diagram
                VbaProject vbaProject = diagram.VbaProject;

                // Output basic VBA project information
                Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
                Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");

                // List all VBA modules and their code
                foreach (VbaModule module in vbaProject.Modules)
                {
                    Console.WriteLine($"--- Module: {module.Name} ---");
                    Console.WriteLine(module.Codes);
                    Console.WriteLine();
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to load a Diagram asynchronously
    private static Task<Diagram> LoadDiagramAsync(string path)
    {
        return Task.Run(() =>
        {
            // Diagram constructor loads the file synchronously; wrapping in Task.Run makes it asynchronous
            return new Diagram(path);
        });
    }
}