using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioDiagramPath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: verify the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Access the VBA project associated with the diagram
                VbaProject vbaProject = diagram.VbaProject;

                // Output the VBA project name
                Console.WriteLine($"VBA Project Name: {vbaProject.Name}");

                // Determine whether the VBA project is digitally signed
                bool isSigned = vbaProject.IsSigned;
                Console.WriteLine($"Is VBA Project Signed: {isSigned}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}