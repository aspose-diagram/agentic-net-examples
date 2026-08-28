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

            // Access the first page (adjust index if needed)
            var page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Identify triangle shapes by their master name
                if (shape.Master != null && shape.Master.Name == "Triangle")
                {
                    // Set solid fill pattern
                    shape.Fill.FillPattern.Value = 1; // 1 = solid

                    // Apply blue fill color (hex format)
                    shape.Fill.FillForegnd.Value = "#0000FF";
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
