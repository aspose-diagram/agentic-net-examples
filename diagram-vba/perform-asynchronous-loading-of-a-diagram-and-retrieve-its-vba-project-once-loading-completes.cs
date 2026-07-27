using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    // Async entry point
    static async Task Main(string[] args)
    {
        try
        {

            // Expect a file path argument
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: <program> <VisioFilePath>");
                return;
            }

            string filePath = args[0];

            // Load the diagram asynchronously
            Diagram diagram = await LoadDiagramAsync(filePath);

            // Retrieve the VBA project
            var vbaProject = diagram.VbaProject;

            // Output basic VBA project information
            Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
            Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");
            Console.WriteLine($"Modules Count: {vbaProject.Modules.Count}");

            // List each module's name and code
            for (int i = 0; i < vbaProject.Modules.Count; i++)
            {
                var module = vbaProject.Modules[i];
                Console.WriteLine($"--- Module {i} ---");
                Console.WriteLine($"Name: {module.Name}");
                Console.WriteLine("Code:");
                Console.WriteLine(module.Codes);
                Console.WriteLine();
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }

    // Helper method to load a diagram on a background thread
    private static Task<Diagram> LoadDiagramAsync(string path)
    {
        return Task.Run(() =>
        {
            // Synchronous constructor call wrapped in a Task
            return new Diagram(path);
        });
    }
}
