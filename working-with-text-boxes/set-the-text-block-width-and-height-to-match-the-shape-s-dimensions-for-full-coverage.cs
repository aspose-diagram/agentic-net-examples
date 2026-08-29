using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all shapes on the first page (adjust as needed)
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                // Ensure the shape has a TextXForm (text block) to modify
                if (shape.TextXForm != null && shape.XForm != null)
                {
                    // Set the text block width to the shape's width
                    shape.TextXForm.TxtWidth.Value = shape.XForm.Width.Value;

                    // Set the text block height to the shape's height
                    shape.TextXForm.TxtHeight.Value = shape.XForm.Height.Value;

                    // Refresh shape data so the changes take effect
                    shape.RefreshData();
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
