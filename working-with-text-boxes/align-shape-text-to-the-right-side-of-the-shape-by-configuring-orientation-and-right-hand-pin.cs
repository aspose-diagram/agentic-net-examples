using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.WriteLine("No shapes found on the page.");
                return;
            }

            // Align the shape's text to the right side
            // Set the local pin of the text block to the left edge (0)
            shape.TextXForm.TxtLocPinX.Value = 0;
            // Position the text block's pin at the shape's right edge
            shape.TextXForm.TxtPinX.Value = shape.XForm.Width.Value;

            // Ensure no rotation is applied to the text
            shape.TextXForm.TxtAngle.Value = 0;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
