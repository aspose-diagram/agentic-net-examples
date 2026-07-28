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
            var diagram = new Diagram("input.vsdx");

            // Get the first page (or any specific page you need)
            var page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Log basic shape identification
                Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

                // Determine whether the shape inherits fill and line formatting
                bool inheritsFill = shape.InheritFill != null;
                bool inheritsLine = shape.InheritLine != null;

                // Log the inheritance flags
                Console.WriteLine($"  InheritFill: {inheritsFill}, InheritLine: {inheritsLine}");
            }

            // Save the diagram (optional, if you made changes)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
