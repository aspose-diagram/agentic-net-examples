using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <legacyDiagramPath> <outputDiagramPath>");
            return;
        }

        string legacyPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the legacy file exists.
        if (!File.Exists(legacyPath))
        {
            Console.Error.WriteLine($"Error: The file '{legacyPath}' does not exist.");
            return;
        }

        try
        {
            // Load the legacy diagram inside a using block as required.
            using (Diagram diagram = new Diagram(legacyPath))
            {
                // The diagram is loaded with all its content, including page-level comments.
                // Save it to the latest VSDX format. Comments are preserved automatically.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Successfully migrated '{legacyPath}' to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred during migration: {ex.Message}");
        }
    }
}