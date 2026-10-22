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
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard: verify the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram from the specified file
            diagram = new Diagram(filePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        try
        {
            // Access the VBA project associated with the diagram
            VbaProject vbaProject = diagram.VbaProject;

            if (vbaProject == null)
            {
                Console.WriteLine("No VBA project found in the diagram.");
            }
            else
            {
                // Output the VBA project name and its signature status
                Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
                Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error accessing VBA project: {ex.Message}");
        }
        finally
        {
            // Dispose the diagram to release resources
            if (diagram != null)
            {
                diagram.Dispose();
            }
        }
    }
}