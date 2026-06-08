using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram("input.vsdx");

            // Obtain a shape – here we use the first page and a shape with a known ID.
            // Adjust the ID or use GetShape(string) as needed.
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Iterate over all fields attached to the shape.
            foreach (Field field in shape.Fields)
            {
                // 'Type' provides the data type of the field (e.g., String, Double, Date).
                // 'Value' contains the actual value stored in the field.
                // 'ObjectKind' can be used to identify the kind of field (Prop, User, etc.).
                Console.WriteLine($"Field Kind: {field.ObjectKind}, Type: {field.Type}, Value: {field.Value}");
            }

            // Save the diagram if any changes were made (lifecycle rule: save)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
