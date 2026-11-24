using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <password>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string password = args[2];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains a VBA project
            if (diagram.VbaProject == null)
            {
                Console.Error.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // TODO: Apply password protection to the VBA project.
            // Aspose.Diagram currently does not expose an API to set a password on the VBA project.
            // When such an API becomes available, it should be invoked here, e.g.:
            // diagram.VbaProject.SetPassword(password);
            // For now, we simply output the intended password (do not store it in the file).

            Console.WriteLine($"Intended VBA password: {password}");
            Console.WriteLine($"VBA Project Name: {diagram.VbaProject.Name}");
            Console.WriteLine($"Is VBA Project Signed: {diagram.VbaProject.IsSigned}");

            // Save the diagram in macro-enabled format (VSDM)
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}