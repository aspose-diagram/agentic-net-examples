using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;

        try
        {
            // Load the Visio diagram from the specified file.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Define the placeholder text to insert into empty shapes.
        const string placeholder = "Placeholder Text";

        try
        {
            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape.
                    string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                    // Check if the shape's text is empty or whitespace.
                    if (string.IsNullOrWhiteSpace(shapeText))
                    {
                        // Clear any existing text fragments.
                        shape.Text.Value.Clear();

                        // Add the placeholder text as a new Txt object.
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

        try
        {
            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}