using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input file path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Attempt to load the diagram; this may throw if master definitions are corrupted
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram '{inputPath}'. Exception: {ex.Message}");
            return;
        }

        // Process masters: list their IDs and names, and detect possible corruption (e.g., missing name)
        Console.WriteLine("Master information:");
        foreach (Master master in diagram.Masters)
        {
            string masterName = master.Name;
            if (string.IsNullOrEmpty(masterName))
            {
                Console.Error.WriteLine($"Warning: Master with ID {master.ID} has an invalid or missing name. It may be corrupted.");
                // Continue processing other masters; you could implement additional recovery logic here
                continue;
            }

            Console.WriteLine($"- ID: {master.ID}, Name: {masterName}");
        }

        // Prepare output path
        string directory = Path.GetDirectoryName(inputPath) ?? "";
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_clean.vsdx");

        try
        {
            // Save the diagram to a new file (unchanged content)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram to '{outputPath}'. Exception: {ex.Message}");
        }
    }
}