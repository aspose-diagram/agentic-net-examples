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

            // Work with the first page (adjust as needed)
            Page page = diagram.Pages[0];

            // Get the target shape (replace 1 with the actual shape ID)
            Shape shape = page.Shapes.GetShape(1);

            // Zero‑based index of the field to delete
            int fieldIndex = 0;

            // Verify the index is within the collection bounds
            if (fieldIndex >= 0 && fieldIndex < shape.Fields.Count)
            {
                // Retrieve the field at the specified index
                Field field = shape.Fields[fieldIndex];

                // Remove the field from the shape's field collection
                shape.Fields.Remove(field);
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
