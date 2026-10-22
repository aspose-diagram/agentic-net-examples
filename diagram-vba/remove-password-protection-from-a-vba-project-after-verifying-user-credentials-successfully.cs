using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths
        string inputPath;
        string outputPath;

        if (args.Length >= 2)
        {
            inputPath = args[0];
            outputPath = args[1];
        }
        else
        {
            Console.Write("Enter the path to the Visio file (macro-enabled .vsdm or .vsdx): ");
            inputPath = Console.ReadLine()?.Trim();

            Console.Write("Enter the desired output file path (e.g., output.vsdm): ");
            outputPath = Console.ReadLine()?.Trim();
        }

        // Guard against missing input file
        if (string.IsNullOrEmpty(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Simple credential verification (hard‑coded for demonstration)
        Console.Write("Enter username: ");
        string username = Console.ReadLine()?.Trim();

        Console.Write("Enter password: ");
        string password = Console.ReadLine()?.Trim();

        // Replace these with real credential checks as needed
        const string validUser = "admin";
        const string validPass = "password";

        if (username != validUser || password != validPass)
        {
            Console.Error.WriteLine("Invalid credentials. Operation aborted.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Check if a VBA project exists
            if (diagram.VbaProject != null)
            {
                // Remove VBA project data (including any password protection)
                // According to Aspose.Diagram guidelines, setting VbProjectData to null clears macros/VBA.
                diagram.VbProjectData = null;

                Console.WriteLine("VBA project data cleared successfully.");
            }
            else
            {
                Console.WriteLine("No VBA project found in the diagram.");
            }

            // Ensure the output directory exists
            string outDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            // Save the modified diagram in a macro‑enabled format
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}