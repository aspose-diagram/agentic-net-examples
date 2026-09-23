using System.IO;
using Aspose.Diagram;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Locate the triangle shape (assumes its NameU is "Triangle")
            Shape triangle = diagram.Pages[0].Shapes.FirstOrDefault(s => s.NameU == "Triangle");

            if (triangle != null)
            {
                // Move the triangle to the desired coordinates (200,150)
                triangle.XForm.PinX.Value = 200;
                triangle.XForm.PinY.Value = 150;
            }
            else
            {
                Console.WriteLine("Triangle shape not found.");
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
