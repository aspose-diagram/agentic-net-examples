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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has a master and inherits fill formatting from it
                    bool inheritsFill = shape.Master != null && shape.InheritFill != null;

                    // Log the inheritance status
                    Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Inherits Fill: {inheritsFill}");
                }
            }

            // Save the diagram (no modifications made, just to follow lifecycle rules)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
