using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for shape operations per global rule

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file to be inspected
        string inputPath = "input.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Define the expected alignment values according to the style guide
            VerticalAlignValue expectedVertical = VerticalAlignValue.Middle;
            HorzAlignValue expectedHorizontal = HorzAlignValue.Center; // Correct enum member

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check vertical alignment of the shape's text block
                    if (shape.TextBlock.VerticalAlign.Value != expectedVertical)
                    {
                        Console.WriteLine($"[Mismatch] Shape ID {shape.ID} ('{shape.NameU}') has vertical alignment '{shape.TextBlock.VerticalAlign.Value}', expected '{expectedVertical}'.");
                    }

                    // Check horizontal alignment of the first paragraph, if any
                    if (shape.Paras.Count > 0)
                    {
                        if (shape.Paras[0].HorzAlign.Value != expectedHorizontal)
                        {
                            Console.WriteLine($"[Mismatch] Shape ID {shape.ID} ('{shape.NameU}') has horizontal alignment '{shape.Paras[0].HorzAlign.Value}', expected '{expectedHorizontal}'.");
                        }
                    }
                    else
                    {
                        // No paragraph information – treat as informational note
                        Console.WriteLine($"[Info] Shape ID {shape.ID} ('{shape.NameU}') has no paragraph data to evaluate horizontal alignment.");
                    }
                }
            }

            Console.WriteLine("Alignment check completed.");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}