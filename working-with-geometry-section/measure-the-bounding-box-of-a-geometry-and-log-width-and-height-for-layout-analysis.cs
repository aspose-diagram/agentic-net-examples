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
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // The XForm property contains the shape's size (width and height)
                    // Width and Height are stored as DoubleValue objects
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Log the bounding box dimensions for layout analysis
                    Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Width: {width}, Height: {height}");
                }
            }

            // Optionally, save the diagram if any modifications were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
