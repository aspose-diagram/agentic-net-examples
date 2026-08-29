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

            // Locate the page named "Details"
            Page detailsPage = null;
            foreach (Page page in diagram.Pages)
            {
                if (page.Name == "Details")
                {
                    detailsPage = page;
                    break;
                }
            }

            // If the page exists, move each shape 20 units to the right (X axis)
            if (detailsPage != null)
            {
                foreach (Shape shape in detailsPage.Shapes)
                {
                    // Move adds the offset to the current position; Y offset is 0
                    shape.Move(20.0, 0.0);
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
