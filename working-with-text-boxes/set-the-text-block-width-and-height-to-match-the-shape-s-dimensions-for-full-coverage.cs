using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Loop through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify that the shape has XForm (size) and TextXForm (text block) objects
                    if (shape.XForm != null && shape.TextXForm != null)
                    {
                        // Set the text block width to the shape's width
                        shape.TextXForm.TxtWidth = shape.XForm.Width;

                        // Set the text block height to the shape's height
                        shape.TextXForm.TxtHeight = shape.XForm.Height;

                        // Refresh shape data so the changes take effect
                        shape.RefreshData();
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
