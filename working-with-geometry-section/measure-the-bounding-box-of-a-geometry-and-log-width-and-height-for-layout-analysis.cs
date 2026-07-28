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
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the bounding box dimensions from the shape's XForm
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Log the page name, shape ID, shape name, width and height
                    Console.WriteLine(
                        $"Page: {page.Name}, Shape ID: {shape.ID}, Name: {shape.Name}, Width: {width}, Height: {height}");
                }
            }

            // Save the diagram (no modifications made, just demonstrating the save lifecycle)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
