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

            // Iterate through shapes on the first page to find a rectangle shape
            Page page = diagram.Pages[0];
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a master and its name is "Rectangle"
                if (shape.Master != null && shape.Master.Name == "Rectangle")
                {
                    // Set the text direction of the shape's text block to vertical
                    shape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;
                }
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
