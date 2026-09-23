using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to find the triangle
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify triangle shapes by their master name
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        // Set line weight to 2 points (2/72 inches)
                        shape.Line.LineWeight.Value = 2.0 / 72.0;
                        // Set line color to navy (hex #000080)
                        shape.Line.LineColor.Value = "#000080";
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
