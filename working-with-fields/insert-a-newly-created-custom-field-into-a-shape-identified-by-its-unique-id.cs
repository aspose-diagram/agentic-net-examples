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

            // Unique ID of the target shape (replace with the actual ID)
            long targetShapeId = 5;

            // Retrieve the shape from the first page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(targetShapeId);

            // Create a new text field (custom field) and set its value
            Field customField = new Field();
            customField.Value.Val = "Custom Field Value";

            // Add the field to the shape's Fields collection
            shape.Fields.Add(customField);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
