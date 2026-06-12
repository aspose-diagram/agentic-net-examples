using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the style guide (expected alignments)
                // For simplicity, the guide expects all shapes to be centered horizontally and middle vertically.
                HorzAlignValue expectedHorz = HorzAlignValue.Center;
                VerticalAlignValue expectedVert = VerticalAlignValue.Middle;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has at least one paragraph to check horizontal alignment
                        if (shape.Paras.Count == 0)
                            continue;

                        // Retrieve current alignments
                        HorzAlignValue currentHorz = shape.Paras[0].HorzAlign.Value;
                        VerticalAlignValue currentVert = shape.TextBlock.VerticalAlign.Value;

                        // Compare with the style guide and log mismatches
                        if (currentHorz != expectedHorz)
                        {
                            Console.WriteLine($"[Mismatch] Shape ID {shape.ID} ({shape.NameU}) - Horizontal alignment is {currentHorz}, expected {expectedHorz}.");
                        }

                        if (currentVert != expectedVert)
                        {
                            Console.WriteLine($"[Mismatch] Shape ID {shape.ID} ({shape.NameU}) - Vertical alignment is {currentVert}, expected {expectedVert}.");
                        }
                    }
                }

                // Optionally, save the diagram (no changes made here)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }