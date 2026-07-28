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

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape (ID typically starts at 1)
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.WriteLine("Unable to retrieve the shape.");
                return;
            }

            // Anchor the text block to the exact center of the shape geometry
            // Set the text block pin to the shape's PinX and PinY (center of the shape)
            shape.TextXForm.TxtPinX.Value = shape.XForm.PinX.Value;
            shape.TextXForm.TxtPinY.Value = shape.XForm.PinY.Value;

            // Adjust the local pin so the text block itself is centered on its pin
            // Use half of the text block's width and height as offsets
            shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value / 2.0;
            shape.TextXForm.TxtLocPinY.Value = shape.TextXForm.TxtHeight.Value / 2.0;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
