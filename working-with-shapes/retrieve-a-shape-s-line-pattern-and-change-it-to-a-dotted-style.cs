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

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Identify the shape to modify (example uses shape ID 1)
            long shapeId = 1;
            Shape shape = page.Shapes.GetShape(shapeId);

            // Retrieve and display the current line pattern
            LinePatternValue currentPattern = shape.Line.LinePattern.Value;
            Console.WriteLine($"Current line pattern: {currentPattern}");

            // Change the line pattern to a dotted style
            shape.Line.LinePattern.Value = LinePatternValue.Dot;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Line pattern changed to dotted and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
