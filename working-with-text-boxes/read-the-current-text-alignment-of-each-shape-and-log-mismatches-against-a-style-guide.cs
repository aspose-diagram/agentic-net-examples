using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // required for shape operations per global rule

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
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
            HorzAlignValue expectedHorz = HorzAlignValue.LeftAlign;   // horizontal left alignment
            VerticalAlignValue expectedVert = VerticalAlignValue.Middle; // vertical middle alignment

            // Iterate through each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text of the shape; skip shapes without text
                    string shapeText = shape.Text.Value.ToString();
                    if (string.IsNullOrWhiteSpace(shapeText))
                        continue;

                    // Determine horizontal alignment (from the first paragraph, if any)
                    HorzAlignValue actualHorz = expectedHorz; // fallback to expected
                    if (shape.Paras.Count > 0)
                        actualHorz = shape.Paras[0].HorzAlign.Value;

                    // Determine vertical alignment (from the TextBlock)
                    VerticalAlignValue actualVert = shape.TextBlock.VerticalAlign.Value;

                    // Check for mismatches against the style guide
                    bool horzMismatch = actualHorz != expectedHorz;
                    bool vertMismatch = actualVert != expectedVert;

                    if (horzMismatch || vertMismatch)
                    {
                        // Log detailed information about the mismatch
                        Console.WriteLine($"Mismatch in Shape ID {shape.ID} on Page {pageIndex}:");
                        if (horzMismatch)
                            Console.WriteLine($"  Horizontal alignment - Expected: {expectedHorz}, Actual: {actualHorz}");
                        if (vertMismatch)
                            Console.WriteLine($"  Vertical alignment   - Expected: {expectedVert}, Actual: {actualVert}");
                        Console.WriteLine($"  Shape Text: \"{shapeText}\"");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}