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

            // Flag to indicate whether a triangle shape was found
            bool triangleFound = false;

            // Iterate through all pages and shapes to locate the triangle shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape uses the "Triangle" master
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        // Rotate the shape 45 degrees clockwise around its center
                        shape.XForm.Angle.Value = 45.0;

                        triangleFound = true;
                        // If multiple triangles need rotation, remove the break statement
                        break;
                    }
                }

                if (triangleFound)
                    break;
            }

            if (!triangleFound)
            {
                Console.WriteLine("Triangle shape not found in the diagram.");
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
