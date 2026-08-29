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

            // Verify that the diagram has at least three pages.
            if (diagram.Pages.Count < 3)
            {
                Console.WriteLine("The diagram does not contain a third page.");
                return;
            }

            // Retrieve the third page (zero‑based index 2).
            Page page = diagram.Pages[2];

            // Rotate the text of every shape on this page by 90 degrees.
            // Text rotation is specified in radians; 90° = π/2.
            foreach (Shape shape in page.Shapes)
            {
                shape.TextXForm.TxtAngle.Value = Math.PI / 2;
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
