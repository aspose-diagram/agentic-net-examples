using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Determine whether the shape has inheritance elements for Fill and Line
                    bool hasInheritFill = shape.InheritFill != null;
                    bool hasInheritLine = shape.InheritLine != null;

                    // Log the shape ID, name, and inheritance flags
                    Console.WriteLine(
                        $"Shape ID: {shape.ID}, Name: {shape.Name}, " +
                        $"InheritFill: {hasInheritFill}, InheritLine: {hasInheritLine}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
