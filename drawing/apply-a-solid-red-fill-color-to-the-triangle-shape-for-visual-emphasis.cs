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

            // Iterate through all pages and shapes to find the triangle shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and that the master name is "Triangle"
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        // Apply a solid fill pattern (value 1) and set the foreground color to solid red
                        shape.Fill.FillPattern.Value = 1;               // Solid fill
                        shape.Fill.FillForegnd.Value = "#FF0000";       // Red color in HEX
                    }
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
