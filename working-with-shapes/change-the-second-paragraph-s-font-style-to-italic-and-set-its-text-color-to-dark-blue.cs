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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains text and at least two paragraphs
                    if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text) && shape.Paras.Count > 1)
                    {
                        // Get the second paragraph (index 1)
                        var secondPara = shape.Paras[1];

                        // Determine the range of character indices that belong to the second paragraph
                        int startIndex = secondPara.IX;
                        int endIndex;

                        if (shape.Paras.Count > 2)
                        {
                            // The start index of the next paragraph marks the end of the current one
                            endIndex = shape.Paras[2].IX;
                        }
                        else
                        {
                            // Last paragraph – use the total number of character runs as the end
                            endIndex = shape.Chars.Count;
                        }

                        // Apply formatting to each character run within the range
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            if (ch.IX >= startIndex && ch.IX < endIndex)
                            {
                                // Set italic style while preserving existing styles
                                ch.Style.Value |= StyleValue.Italic;

                                // Set text color to dark blue (hex #00008B)
                                ch.Color.Value = "#00008B";
                            }
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
