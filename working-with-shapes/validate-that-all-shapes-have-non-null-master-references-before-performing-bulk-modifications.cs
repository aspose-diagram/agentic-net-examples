using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Prepare an output path with a "_modified" suffix
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool allShapesValid = true;

                // Validate that every non-deleted shape has a non-null master reference
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check master reference
                        if (shape.Master == null)
                        {
                            Console.Error.WriteLine(
                                $"Validation error: Shape ID {shape.ID} on page \"{page.Name}\" has a null master reference.");
                            allShapesValid = false;
                        }
                    }
                }

                // Abort if validation failed
                if (!allShapesValid)
                {
                    Console.Error.WriteLine("Validation failed: some shapes lack master references. No modifications applied.");
                    return;
                }

                // Bulk modification example: set fill foreground color to red for all shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Master is guaranteed non-null after validation
                        shape.Fill.FillForegnd.Value = "#FF0000"; // Red color
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram successfully saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}