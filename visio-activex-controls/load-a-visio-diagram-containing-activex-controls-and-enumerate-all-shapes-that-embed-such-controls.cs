using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (can be .vsdx, .vsd, etc.)
            string visioFilePath = "input.vsdx";

            // Load the Visio diagram using the built‑in constructor (lifecycle rule)
            Diagram diagram = new Diagram(visioFilePath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape embeds an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Output basic information about the shape
                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Name: {shape.Name}");
                    }
                }
            }

            // Dispose the diagram when done
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
