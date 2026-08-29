using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram (assumes a load rule is defined elsewhere)
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // The name of the shape we want to locate (case‑insensitive)
            string shapeNameToFind = "MyShape";

            // Variable that will hold the found shape
            Aspose.Diagram.Shape foundShape = null;

            // Iterate through all shapes on the first page (adjust page index as needed)
            foreach (Aspose.Diagram.Shape shape in diagram.Pages[0].Shapes)
            {
                // Compare shape names ignoring case
                if (string.Equals(shape.Name, shapeNameToFind, System.StringComparison.OrdinalIgnoreCase))
                {
                    foundShape = shape;
                    break; // Exit loop once the shape is found
                }
            }

            // At this point 'foundShape' contains the shape with the specified name (or null if not found)

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
