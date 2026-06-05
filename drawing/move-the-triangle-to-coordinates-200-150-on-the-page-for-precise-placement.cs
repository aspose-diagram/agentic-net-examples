using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Locate the triangle shape by its master name
                Shape triangle = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        triangle = shape;
                        break;
                    }
                }

                if (triangle == null)
                    throw new Exception("Triangle shape not found.");

                // Move the triangle to the desired coordinates (200, 150)
                triangle.XForm.PinX.Value = 200.0;
                triangle.XForm.PinY.Value = 150.0;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
