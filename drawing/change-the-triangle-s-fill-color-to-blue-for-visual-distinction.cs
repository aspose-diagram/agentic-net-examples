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

            // Iterate through all pages and shapes to find triangle shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify a triangle by its master name
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        // Set solid fill pattern
                        shape.Fill.FillPattern.Value = 1; // 1 = solid
                        // Set fill foreground color to blue (hex format)
                        shape.Fill.FillForegnd.Value = "#0000FF";
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
