using System;
using System.IO;
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

            // If the page exists, shift all its shapes 20 units right (X axis)
            if (detailsPage != null)
            {
                foreach (Shape shape in detailsPage.Shapes)
                {
                    shape.Move(20.0, 0.0); // dX = 20, dY = 0
                }
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
