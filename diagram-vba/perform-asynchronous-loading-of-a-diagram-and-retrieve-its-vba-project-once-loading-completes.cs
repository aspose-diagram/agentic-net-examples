using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static async Task Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Asynchronously load the diagram
            Diagram diagram = await LoadDiagramAsync(diagramPath);

            // Retrieve the VBA project from the loaded diagram
            VbaProject vbaProject = diagram.VbaProject;

            if (vbaProject == null)
            {
                Console.WriteLine("The diagram does not contain a VBA project.");
            }
            else
            {
                // Output basic VBA project information
                Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
                Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");

                // List all VBA modules within the project
                Console.WriteLine("VBA Modules:");
                foreach (VbaModule module in vbaProject.Modules)
                {
                    Console.WriteLine($"- Module Name: {module.Name}");
                    Console.WriteLine("  Code:");
                    Console.WriteLine(module.Codes);
                }
            }

            // Dispose the diagram to free resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads a Visio diagram from the specified file path on a background thread.
    /// </summary>
    /// <param name="path">The full path to the Visio file.</param>
    /// <returns>A Task that resolves to the loaded Diagram instance.</returns>
    private static Task<Diagram> LoadDiagramAsync(string path)
    {
        return Task.Run(() =>
        {
            // The Diagram constructor loads the file synchronously.
            // Wrapping it in Task.Run provides asynchronous behavior.
            return new Diagram(path);
        });
    }
}