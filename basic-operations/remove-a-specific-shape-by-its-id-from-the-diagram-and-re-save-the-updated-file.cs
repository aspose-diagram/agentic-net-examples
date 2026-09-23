using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Specify the ID of the shape to be removed
            int shapeIdToRemove = 5; // replace with the actual shape ID

            // Locate the shape on the first page (adjust page index if needed)
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeIdToRemove);

            // Remove the shape if it exists
            if (shape != null)
            {
                diagram.Pages[0].Shapes.Remove(shape);
            }

            // Save the updated diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
