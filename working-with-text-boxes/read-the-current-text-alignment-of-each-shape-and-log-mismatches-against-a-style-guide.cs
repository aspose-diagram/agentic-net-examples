using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the style guide expectations.
                // Example guide: Horizontal alignment should be Center, vertical alignment should be Middle.
                HorzAlignValue expectedHorz = HorzAlignValue.Center;
                VerticalAlignValue expectedVert = VerticalAlignValue.Middle;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape identifier and name for logging.
                        string shapeId = shape.ID.ToString();
                        string shapeName = shape.NameU ?? "(no name)";

                        bool horzMismatch = false;
                        bool vertMismatch = false;

                        // ----- Horizontal Alignment Check -----
                        HorzAlignValue actualHorz = expectedHorz; // default to expected to avoid uninitialized warning
                        if (shape.Paras != null && shape.Paras.Count > 0)
                        {
                            // Use the first paragraph's alignment as representative.
                            actualHorz = shape.Paras[0].HorzAlign.Value;
                            if (actualHorz != expectedHorz)
                                horzMismatch = true;
                        }
                        else
                        {
                            // No paragraph defined; treat as mismatch.
                            horzMismatch = true;
                        }

                        // ----- Vertical Alignment Check -----
                        VerticalAlignValue actualVert = shape.TextBlock.VerticalAlign.Value;
                        if (actualVert != expectedVert)
                            vertMismatch = true;

                        // Log mismatches.
                        if (horzMismatch || vertMismatch)
                        {
                            Console.WriteLine($"Shape ID: {shapeId}, Name: {shapeName}");
                            if (horzMismatch)
                                Console.WriteLine($"  Horizontal alignment mismatch. Actual: {actualHorz}, Expected: {expectedHorz}");
                            if (vertMismatch)
                                Console.WriteLine($"  Vertical alignment mismatch. Actual: {actualVert}, Expected: {expectedVert}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}