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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Skip shapes that have no text content
                    if (string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                        continue;

                    // Align paragraph text to the left
                    if (shape.Paras.Count > 0)
                    {
                        shape.Paras[0].HorzAlign.Value = HorzAlignValue.LeftAlign;
                    }

                    // Position the text block at the left side of the shape
                    // TxtLocPinX = 0 aligns the text block's local pin to the left edge
                    // TxtPinX = 0 places the text block's pin at the shape's left boundary
                    shape.TextXForm.TxtLocPinX.Value = 0;
                    shape.TextXForm.TxtPinX.Value = 0;
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
