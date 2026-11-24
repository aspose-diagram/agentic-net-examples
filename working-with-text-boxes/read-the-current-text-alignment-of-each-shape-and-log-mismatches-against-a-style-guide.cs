using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Define the expected alignment values according to the style guide
                // Horizontal alignment: Center
                // Vertical alignment: Middle
                HorzAlignValue expectedHorz = HorzAlignValue.Center;
                VerticalAlignValue expectedVert = VerticalAlignValue.Middle;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine actual horizontal alignment (use first paragraph if available)
                        HorzAlignValue actualHorz = HorzAlignValue.LeftAlign; // fallback default
                        if (shape.Paras != null && shape.Paras.Count > 0)
                        {
                            // Access the first paragraph's horizontal alignment
                            actualHorz = shape.Paras[0].HorzAlign.Value;
                        }

                        // Determine actual vertical alignment from the text block
                        VerticalAlignValue actualVert = shape.TextBlock.VerticalAlign.Value;

                        // Check for mismatches against the style guide
                        bool horzMismatch = actualHorz != expectedHorz;
                        bool vertMismatch = actualVert != expectedVert;

                        if (horzMismatch || vertMismatch)
                        {
                            // Log detailed information about the mismatch
                            string shapeName = shape.NameU ?? "(no name)";
                            Console.WriteLine($"Mismatch in Shape ID {shape.ID}, Name \"{shapeName}\":");

                            if (horzMismatch)
                            {
                                Console.WriteLine($"  Horizontal Alignment - Expected: {expectedHorz}, Actual: {actualHorz}");
                            }

                            if (vertMismatch)
                            {
                                Console.WriteLine($"  Vertical Alignment   - Expected: {expectedVert}, Actual: {actualVert}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
