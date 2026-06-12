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

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page (ID 1)
                // Adjust the ID as needed for your specific diagram
                Shape shape = page.Shapes.GetShape(1);

                // Ensure the shape is not marked as deleted
                if (shape.Del == BOOL.False)
                {
                    // Get the current line pattern
                    LinePatternValue currentPattern = shape.Line.LinePattern.Value;
                    Console.WriteLine($"Current line pattern: {currentPattern}");

                    // Change the line pattern to a dotted style
                    // Assuming the enum contains a Dot member for dotted lines
                    shape.Line.LinePattern.Value = LinePatternValue.Dot;

                    Console.WriteLine("Line pattern changed to dotted.");
                }
                else
                {
                    Console.WriteLine("The selected shape is deleted and cannot be modified.");
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