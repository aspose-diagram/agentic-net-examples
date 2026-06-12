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

            // Iterate through each page and each shape on the page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Determine if the shape has a master and an inherited fill definition
                    bool inheritsFill = shape.Master != null && shape.InheritFill != null;

                    // Log the inheritance status
                    Console.WriteLine($"Shape ID {shape.ID} on Page ID {page.ID} inherits fill from master: {inheritsFill}");
                }
            }

            // Save the diagram (optional, demonstrates usage of save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
