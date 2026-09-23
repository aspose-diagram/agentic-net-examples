using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Locate the shape whose field needs to be refreshed.
            // Here we look for a shape with the universal name "Rectangle".
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "Rectangle")
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("Target shape not found.");
                return;
            }

            // Modify the shape's geometry (example: set new width and height in inches)
            targetShape.XForm.Width.Value = 2.0;   // New width
            targetShape.XForm.Height.Value = 1.0;  // New height

            // Refresh the shape's field data so that any dependent fields reflect the new geometry
            targetShape.RefreshData();

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with refreshed field values.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
