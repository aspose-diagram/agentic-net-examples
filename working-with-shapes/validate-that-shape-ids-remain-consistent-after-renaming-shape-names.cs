using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio file
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Path for the output file (optional, to verify saving works)
        string outputPath = "output_renamed.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Use the active page for this example
            Page page = diagram.ActivePage;

            // Store original shape IDs and names for validation
            List<long> originalIds = new List<long>();
            List<string> originalNames = new List<string>();

            // Iterate over all shapes on the page, ignoring logically deleted ones
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Record the original ID and name
                originalIds.Add(shape.ID);
                originalNames.Add(shape.NameU);
            }

            // Rename each shape by appending "_Renamed" to its NameU
            int index = 0;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.True)
                    continue;

                // Perform the rename
                shape.NameU = originalNames[index] + "_Renamed";
                index++;
            }

            // Validate that each shape's ID remains unchanged after renaming
            index = 0;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.True)
                    continue;

                long expectedId = originalIds[index];
                if (shape.ID != expectedId)
                {
                    // If an ID mismatch is detected, throw an exception
                    throw new Exception($"Shape ID mismatch after rename. Expected: {expectedId}, Actual: {shape.ID}");
                }
                index++;
            }

            // If validation passes, optionally save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Shape renaming completed successfully. IDs are consistent.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}