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
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a text block
                        if (shape.Text == null || shape.Text.Value == null)
                            continue;

                        // Align text to the right side of the shape
                        // Set the local pin of the text block to the right edge (0 means right‑hand side)
                        shape.TextXForm.TxtLocPinX.Value = 0;
                        // Position the text block pin at the shape's right edge
                        shape.TextXForm.TxtPinX.Value = shape.XForm.Width.Value;

                        // Optional: keep vertical positioning centered
                        shape.TextXForm.TxtLocPinY.Value = shape.XForm.Height.Value / 2;
                        shape.TextXForm.TxtPinY.Value = shape.XForm.Height.Value / 2;

                        // Ensure no rotation (orientation) is applied
                        shape.TextXForm.TxtAngle.Value = 0;
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
