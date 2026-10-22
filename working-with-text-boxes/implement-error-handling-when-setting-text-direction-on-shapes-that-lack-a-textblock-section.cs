using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument or default suffix)
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape has a TextBlock section
                    if (shape.TextBlock == null)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' does not have a TextBlock section. Skipping.");
                        continue;
                    }

                    try
                    {
                        // Set the text direction to vertical
                        shape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;
                        Console.WriteLine($"Set vertical text direction for shape ID {shape.ID} on page '{page.Name}'.");
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occur while setting the property
                        Console.Error.WriteLine($"Failed to set text direction for shape ID {shape.ID} on page '{page.Name}': {ex.Message}");
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // General error handling for Aspose operations
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}