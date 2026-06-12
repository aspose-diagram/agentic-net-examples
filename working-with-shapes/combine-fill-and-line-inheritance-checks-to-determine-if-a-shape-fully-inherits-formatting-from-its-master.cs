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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Determine if the shape inherits its fill formatting from the master
                    bool inheritsFill = shape.InheritFill != null && shape.Fill == null;

                    // Determine if the shape inherits its line formatting from the master
                    bool inheritsLine = shape.InheritLine != null && shape.Line == null;

                    // If both fill and line are inherited, the shape fully inherits formatting
                    if (inheritsFill && inheritsLine)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} fully inherits formatting from its master.");
                    }
                }
            }

            // Save the diagram (if any modifications were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
