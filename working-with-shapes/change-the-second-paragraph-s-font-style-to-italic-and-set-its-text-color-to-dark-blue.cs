using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Work on the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has at least two paragraphs
                if (shape.Paras.Count >= 2)
                {
                    // Get the second paragraph (zero‑based index)
                    var secondPara = shape.Paras[1];
                    int startIdx = secondPara.IX;

                    // Determine the start index of the next paragraph (if any) to define the range
                    int endIdx = shape.Paras.Count > 2 ? shape.Paras[2].IX : int.MaxValue;

                    // Apply formatting to all character runs that belong to the second paragraph
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        if (ch.IX >= startIdx && ch.IX < endIdx)
                        {
                            // Set italic style while preserving existing styles
                            ch.Style.Value |= StyleValue.Italic;

                            // Set text color to dark blue (hex #00008B)
                            ch.Color.Value = "#00008B";
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
