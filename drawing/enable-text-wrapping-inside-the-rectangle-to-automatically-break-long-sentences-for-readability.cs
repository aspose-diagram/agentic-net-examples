using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the first page (or specify by name/index as needed)
                Page page = diagram.Pages[0];

                // Identify the rectangle shape by its ID.
                // Adjust the shape ID according to your diagram.
                long shapeId = 1; // Example ID; replace with actual rectangle shape ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Ensure the shape is a rectangle (optional check)
                // Note: Shape type checking is omitted due to API limitations.

                // Set a long text string to demonstrate wrapping
                string longText = "This is a very long sentence that should automatically wrap inside the rectangle shape to improve readability and ensure that the text does not overflow the shape boundaries.";
                // Clear existing text
                shape.Text.Value.Clear();
                // Add the new text
                shape.Text.Value.Add(new Txt(longText));

                // Enable text wrapping by defining the text block width.
                // The TxtWidth property controls the width of the text block; setting it to the shape's width forces wrapping.
                shape.TextXForm.TxtWidth.Value = shape.XForm.Width.Value;

                // Optionally, set the text block height to match the shape's height
                shape.TextXForm.TxtHeight.Value = shape.XForm.Height.Value;

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}