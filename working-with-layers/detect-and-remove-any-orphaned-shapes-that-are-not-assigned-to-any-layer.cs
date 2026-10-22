using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input file path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as an argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output path (original name with _cleaned suffix)
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_cleaned.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect shapes to delete to avoid modifying collection during iteration
                var shapesToDelete = new System.Collections.Generic.List<Shape>();

                // Iterate through shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the layer membership string (comma‑separated list of layer indexes)
                    string layerMember = shape.LayerMem.LayerMember.Value;

                    // If the shape is not assigned to any layer, mark it for deletion
                    if (string.IsNullOrWhiteSpace(layerMember))
                    {
                        shapesToDelete.Add(shape);
                    }
                }

                // Delete the identified orphaned shapes
                foreach (Shape orphan in shapesToDelete)
                {
                    // Mark the shape as deleted
                    orphan.Del = BOOL.True;
                    Console.WriteLine($"Removed orphaned shape ID {orphan.ID} from page {page.ID}.");
                }
            }

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Cleaned diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}