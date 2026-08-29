using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // The ID of the shape whose dimensions are required
            long shapeId = 5; // replace with the actual shape ID

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page (adjust if the shape is on a different page)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Ensure the shape exists before accessing its properties
            if (shape != null)
            {
                // Width and Height are stored in the XForm cell collection (values are in inches)
                double width = shape.XForm.Width.Value;
                double height = shape.XForm.Height.Value;

                Console.WriteLine($"Shape ID {shapeId} - Width: {width} inches, Height: {height} inches");
            }
            else
            {
                Console.WriteLine($"Shape with ID {shapeId} was not found on page {page.Name}.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
