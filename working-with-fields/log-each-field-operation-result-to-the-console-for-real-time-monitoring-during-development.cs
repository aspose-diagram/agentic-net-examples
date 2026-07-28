using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Access the first page of the diagram
        Page page = diagram.Pages[0];

        // Add a rectangle shape to the page and capture its ID
        long shapeId = page.DrawRectangle(1, 1, 2, 2);
        Shape shape = page.Shapes.GetShape(shapeId);
        Console.WriteLine($"Added shape with ID: {shapeId}");

        // Create a new text field and add it to the shape
        Field field = new Field();
        shape.Fields.Add(field);
        Console.WriteLine($"Field added. Total fields count: {shape.Fields.Count}");

        // Set the initial value of the field
        field.Value.Val = "Initial Value";
        Console.WriteLine($"Field value set to: {field.Value.Val}");

        // Update the field's value
        field.Value.Val = "Updated Value";
        Console.WriteLine($"Field value updated to: {field.Value.Val}");

        // Remove the field from the shape
        shape.Fields.Remove(field);
        Console.WriteLine($"Field removed. Total fields count: {shape.Fields.Count}");

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        Console.WriteLine("Diagram saved to output.vsdx");
    }
}
