using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that both input and output paths are provided.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        // Assign the input file path and ensure the file exists.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign the output file path and ensure its directory exists.
        string outputPath = args[1];
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Load the Visio diagram inside a try/catch to capture loading errors.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Define the placeholder text that will replace empty shape text.
        const string placeholder = "Placeholder";

        // Iterate through each page and each shape to locate empty text fields.
        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True) continue;

                    // Retrieve the concatenated plain text of the shape.
                    string shapeText = shape.Text.Value.ToString();

                    // If the text is null, empty, or whitespace, replace it.
                    if (string.IsNullOrWhiteSpace(shapeText))
                    {
                        // Remove any existing text runs.
                        shape.Text.Value.Clear();

                        // Add a new text run containing the placeholder.
                        shape.Text.Value.Add(new Txt(placeholder));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing shapes: {ex.Message}");
            return;
        }

        // Save the modified diagram to the specified output path.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            return;
        }

        // Inform the user that processing completed successfully.
        Console.WriteLine("Diagram processing completed successfully.");
    }
}