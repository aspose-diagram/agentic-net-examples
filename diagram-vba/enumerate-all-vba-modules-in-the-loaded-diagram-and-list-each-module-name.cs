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
            Console.Error.WriteLine("Usage: Program <path-to-visio-diagram>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Check if the diagram contains a VBA project
            if (diagram.VbaProject == null)
            {
                Console.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Enumerate all VBA modules and output their names
            Console.WriteLine("VBA Modules in the diagram:");
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                Console.WriteLine($"- {module.Name}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}