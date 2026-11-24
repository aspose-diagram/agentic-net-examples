using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Optional output path for the modified diagram
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "RenamedShapes.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Use the active page (first page) for demonstration
            Page page = diagram.ActivePage;

            // Store original shape IDs in an array for later verification
            long[] originalIds = new long[page.Shapes.Count];
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shape = page.Shapes[i];
                originalIds[i] = shape.ID;
            }

            // Rename each shape's universal name (NameU) while preserving its ID
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shape = page.Shapes[i];
                // Assign a new name that includes the shape ID for clarity
                shape.NameU = $"Renamed_{shape.ID}";
            }

            // Verify that shape IDs remain unchanged after renaming
            bool allIdsConsistent = true;
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shape = page.Shapes[i];
                if (shape.ID != originalIds[i])
                {
                    allIdsConsistent = false;
                    Console.Error.WriteLine($"Shape ID mismatch: expected {originalIds[i]}, found {shape.ID}");
                }
            }

            if (allIdsConsistent)
            {
                Console.WriteLine("All shape IDs remain consistent after renaming.");
            }
            else
            {
                Console.Error.WriteLine("One or more shape IDs changed after renaming.");
            }

            // Save the modified diagram to a new file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}