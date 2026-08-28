using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to locate the triangle
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify the triangle shape (by its universal name)
                    if (shape.NameU == "Triangle")
                    {
                        // Set fill background transparency to 0.25 (75% opacity)
                        shape.Fill.FillBkgndTrans.Value = 0.25;
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
