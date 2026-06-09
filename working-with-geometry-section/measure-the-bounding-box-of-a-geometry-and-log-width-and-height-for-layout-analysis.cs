using System.IO;
using System;
using Aspose.Diagram;

class BoundingBoxAnalyzer
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve width and height from the shape's XForm (bounding box)
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Log the dimensions for layout analysis
                    Console.WriteLine($"Shape ID: {shape.ID}, Width: {width}, Height: {height}");
                }
            }

            // Optionally, save the diagram if any modifications were made
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
