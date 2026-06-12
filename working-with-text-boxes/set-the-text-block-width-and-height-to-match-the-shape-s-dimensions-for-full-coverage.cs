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

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all shapes on each page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a TextXForm (most shapes do)
                    if (shape.TextXForm != null && shape.XForm != null)
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

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
