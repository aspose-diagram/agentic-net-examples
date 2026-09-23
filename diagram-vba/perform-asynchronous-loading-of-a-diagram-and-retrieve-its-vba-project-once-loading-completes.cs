using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the diagram file path as an argument.");
                return;
            }

            string diagramPath = args[0];

            // Asynchronously load the diagram
            Diagram diagram = await LoadDiagramAsync(diagramPath);

            // Retrieve the VBA project from the loaded diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Output basic information about the VBA project
            Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
            Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");
            Console.WriteLine($"Number of Modules: {vbaProject.Modules.Count}");

            // List each module's name and code length
            for (int i = 0; i < vbaProject.Modules.Count; i++)
            {
                var module = vbaProject.Modules[i];
                int codeLength = module.Codes != null ? module.Codes.Length : 0;
                Console.WriteLine($"Module {i}: Name = {module.Name}, Code Length = {codeLength}");
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }

    private static Task<Diagram> LoadDiagramAsync(string path)
    {
        return Task.Run(() =>
        {
            // Load the diagram synchronously within a background task
            return new Diagram(path);
        });
    }
}
