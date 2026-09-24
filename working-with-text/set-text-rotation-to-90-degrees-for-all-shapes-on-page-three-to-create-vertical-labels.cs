using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Verify that the diagram has at least three pages
            if (diagram.Pages.Count < 3)
            {
                Console.WriteLine("The diagram does not contain a third page.");
                diagram.Dispose();
                return;
            }

            // Access page three (zero‑based index 2)
            Page page = diagram.Pages[2];

            // 90 degrees expressed in radians
            double angleRadians = Math.PI / 2.0;

            // Apply the rotation to every shape on the page
            foreach (Aspose.Diagram.Shape shape in page.Shapes)
            {
                shape.TextXForm.TxtAngle.Value = angleRadians;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Text rotation set to 90° for all shapes on page three and saved to output.vsdx.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
