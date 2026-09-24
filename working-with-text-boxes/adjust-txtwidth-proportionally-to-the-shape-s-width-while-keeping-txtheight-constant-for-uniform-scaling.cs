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

            // Load the Visio diagram from a file.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Adjust the text block width (TxtWidth) to be proportional to the shape's width.
                    // This keeps the text scaling uniform while preserving the original TxtHeight.
                    double shapeWidth = shape.XForm.Width.Value;
                    shape.TextXForm.TxtWidth.Value = shapeWidth;

                    // TxtHeight is left unchanged to maintain its constant value.
                }
            }

            // Save the modified diagram.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
