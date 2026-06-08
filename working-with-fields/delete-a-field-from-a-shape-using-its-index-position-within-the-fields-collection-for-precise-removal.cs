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

            // Access the target shape (example: first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Index of the field to be removed (zero‑based)
            int fieldIndex = 0; // adjust as needed

            // Verify that the index is valid
            if (fieldIndex >= 0 && fieldIndex < shape.Fields.Count)
            {
                // Retrieve the field at the specified index
                Field fieldToRemove = shape.Fields[fieldIndex];

                // Remove the field from the shape's field collection
                shape.Fields.Remove(fieldToRemove);
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
