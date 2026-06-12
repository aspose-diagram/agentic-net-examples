using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            var diagram = new Diagram("input.vsdx");

            // Assume we work with the first page; adjust index as needed
            var page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve inheritance information for Fill and Line
                var inheritFill = shape.InheritFill;
                var inheritLine = shape.InheritLine;

                // Prepare readable flag values (null checks to avoid exceptions)
                string fillInfo = inheritFill != null ? "Has InheritFill" : "No InheritFill";
                string lineInfo = inheritLine != null ? "Has InheritLine" : "No InheritLine";

                // Log shape identifier and inheritance flags
                Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
                Console.WriteLine($"  Fill Inheritance: {fillInfo}");
                Console.WriteLine($"  Line Inheritance: {lineInfo}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
