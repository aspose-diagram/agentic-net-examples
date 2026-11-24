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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over the shape collection.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a TextXForm section (all shapes do).
                    // Set text orientation angle to 0 (horizontal).
                    shape.TextXForm.TxtAngle.Value = 0;

                    // Align text to the left side of the shape.
                    // TxtLocPinX defines the local pin (reference point) inside the text block.
                    // Setting it to the text block width positions the text block's left edge at the shape's left edge.
                    shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value;

                    // TxtPinX defines the shape‑relative X coordinate of the text block origin.
                    // Setting it to 0 places the text block at the shape's left boundary.
                    shape.TextXForm.TxtPinX.Value = 0;
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}