using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            var diagram = new Diagram("input.vsdx");

            // Access the first page in the diagram
            var page = diagram.Pages[0];

            // Locate the first shape that is not marked as deleted
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No non‑deleted shape found on the page.");
                return;
            }

            // Retrieve and display the current line pattern of the shape
            var currentPattern = targetShape.Line.LinePattern.Value;
            Console.WriteLine($"Current line pattern: {currentPattern}");

            // Change the line pattern to a dotted style
            targetShape.Line.LinePattern.Value = LinePatternValue.Dot;

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Line pattern updated to dotted and diagram saved as output.vsdx.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
