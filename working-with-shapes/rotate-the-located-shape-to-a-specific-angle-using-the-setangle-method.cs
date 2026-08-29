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

            // Access the first page (you can change the index as needed)
            Page page = diagram.Pages[0];

            // Locate the shape by its universal name (NameU)
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "MyShape")
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("Shape with NameU 'MyShape' not found.");
            }

            // Rotate the shape to the desired angle (degrees)
            double angleInDegrees = 45.0;
            targetShape.SetAngle(angleInDegrees);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
