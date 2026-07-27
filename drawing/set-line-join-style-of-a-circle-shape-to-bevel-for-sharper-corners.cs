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

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Find shapes that use the "Ellipse" master (circle/ellipse shapes)
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Ellipse")
                {
                    // Set line rounding to 0 to achieve a bevel‑like sharp corner effect
                    shape.Line.Rounding.Value = 0;
                    Console.WriteLine($"Shape ID {shape.ID} line rounding set to 0.");
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
