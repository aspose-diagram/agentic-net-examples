using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        string sourcePath = "input.vsdx";          // Path to the source Visio file
        if (!System.IO.File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }
        string destinationPath = "output.vsdx";    // Path where the modified file will be saved

        // Load the Visio diagram (uses the provided load rule)
        Diagram diagram = new Diagram(sourcePath);

        // Identify the shape that contains the field to be removed.
        // Here we assume the shape is on the first page and has a known Shape ID.
        int shapeId = 1; // Replace with the actual Shape ID
        Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

        // Specify the zero‑based index of the field to delete.
        int fieldIndex = 0; // Replace with the desired field index

        // Ensure the index is within the collection bounds.
        if (fieldIndex >= 0 && fieldIndex < shape.Fields.Count)
        {
            // Retrieve the Field object at the specified index.
            Field fieldToRemove = shape.Fields[fieldIndex];

            // Remove the Field from the collection (uses FieldCollection.Remove method).
            shape.Fields.Remove(fieldToRemove);
        }

        // Save the modified diagram (uses the provided save rule)
        diagram.Save(destinationPath, SaveFileFormat.Vsdx);
    }
}
