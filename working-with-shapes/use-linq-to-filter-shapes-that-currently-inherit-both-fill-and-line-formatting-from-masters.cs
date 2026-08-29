using System.IO;
using Aspose.Diagram;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // LINQ: select shapes that inherit both fill and line formatting from their master
            var shapesInheritingBoth = page.Shapes
                .Cast<Shape>()
                .Where(s => s.InheritFill != null && s.InheritLine != null)
                .ToList();

            // Example output: list the IDs and names of the filtered shapes
            foreach (var shape in shapesInheritingBoth)
            {
                Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
            }

            // Save the diagram (if any changes were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
