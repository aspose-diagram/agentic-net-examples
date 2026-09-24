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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a TextXForm (text block) to modify
                    if (shape.TextXForm != null)
                    {
                        // Set text rotation to 90 degrees (in radians)
                        shape.TextXForm.TxtAngle.Value = Math.PI / 2.0;

                        // Position text on the left side of the shape
                        // Align the right edge of the text block with the left edge of the shape
                        shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value; // local pin at right edge of text block
                        shape.TextXForm.TxtPinX.Value = 0.0; // place text block at shape's left border

                        // Vertically center the text within the shape
                        shape.TextXForm.TxtLocPinY.Value = shape.TextXForm.TxtHeight.Value / 2.0;
                        shape.TextXForm.TxtPinY.Value = shape.XForm.Height.Value / 2.0;
                    }
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
