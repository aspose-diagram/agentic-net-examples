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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    // Get the current page.
                    Page page = diagram.Pages[pageIndex];

                    // Iterate over all shapes on the page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Ensure the shape has a valid XForm (size) and TextXForm (text block).
                        // Set the text block width and height to match the shape's width and height.
                        shape.TextXForm.TxtWidth.Value = shape.XForm.Width.Value;
                        shape.TextXForm.TxtHeight.Value = shape.XForm.Height.Value;
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}