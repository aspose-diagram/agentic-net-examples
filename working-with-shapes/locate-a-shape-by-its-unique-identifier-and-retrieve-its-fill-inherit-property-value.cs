using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Specify the unique identifier of the shape (replace with the actual ID)
            int shapeId = 5;

            // Locate the shape by its ID, including any child shapes
            Shape shape = diagram.Pages[0].Shapes.GetShapeIncludingChild(shapeId);

            // Retrieve the InheritFill property (contains inherited fill formatting)
            Fill inheritFill = shape.InheritFill;

            // Example: display some inherited fill properties
            Console.WriteLine($"FillPattern: {inheritFill.FillPattern}");
            Console.WriteLine($"Foreground Color: {inheritFill.FillForegnd}");
            Console.WriteLine($"Background Color: {inheritFill.FillBkgnd}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
