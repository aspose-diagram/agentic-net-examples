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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Choose the page (first page in this example)
            Page page = diagram.Pages[0];

            // Locate the target shape (by shape ID or index)
            // Here we retrieve the shape with ID 1 as an example
            Shape shape = page.Shapes.GetShape(1);

            // Index of the field to remove (zero‑based)
            int fieldIndex = 0; // change as needed

            // Ensure the shape has enough fields
            if (shape.Fields.Count > fieldIndex)
            {
                // Retrieve the field at the specified index
                Field fieldToRemove = shape.Fields[fieldIndex];

                // Remove the field from the shape
                shape.Fields.Remove(fieldToRemove);

                Console.WriteLine($"Field at index {fieldIndex} removed from shape ID {shape.ID}.");
            }
            else
            {
                Console.WriteLine($"Shape ID {shape.ID} does not contain a field at index {fieldIndex}.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
