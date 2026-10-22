using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeId> <newMasterId>");
            return;
        }

        // Input diagram file path.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse shape ID (long) and new master ID (int).
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine("Error: Invalid shape ID. It must be a numeric value.");
            return;
        }

        if (!int.TryParse(args[2], out int newMasterId))
        {
            Console.Error.WriteLine("Error: Invalid master ID. It must be a numeric value.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the target master; if it does not exist GetMaster will return null.
                Master targetMaster = diagram.Masters.GetMaster(newMasterId);
                if (targetMaster == null)
                {
                    Console.Error.WriteLine($"Error: Master with ID {newMasterId} does not exist in the diagram.");
                    return;
                }

                // Retrieve the shape by its ID from the first page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on the first page.");
                    return;
                }

                // Replace the shape's master with the new master.
                shape.Master = targetMaster;

                // Prepare output file path.
                string directory = Path.GetDirectoryName(inputPath) ?? string.Empty; // Ensure non‑null directory.
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, fileNameWithoutExt + "_modified.vsdx");

                // Save the modified diagram using the correct overload.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shape (ID: {shapeId}) master replaced with Master ID {newMasterId}.");
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
