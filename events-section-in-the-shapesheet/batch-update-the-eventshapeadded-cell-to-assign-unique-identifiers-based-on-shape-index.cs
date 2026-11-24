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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path (optional second argument)
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            string directory = Path.GetDirectoryName(inputPath) ?? "";
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(directory, $"{filenameWithoutExt}_updated.vsdx");
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Counter to generate unique identifiers across all shapes
            int shapeCounter = 1;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // The EventShapeAdded cell does not exist in the Aspose.Diagram API.
                    // As a closest substitute, we use the EventDrop cell (shape.Event.EventDrop.Ufe.F)
                    // to store a unique identifier formula for each shape.
                    // The formula is a quoted string like "ID_1", "ID_2", etc.
                    shape.Event.EventDrop.Ufe.F = $"\"ID_{shapeCounter}\"";

                    shapeCounter++;
                }
            }

            // Save the updated diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}