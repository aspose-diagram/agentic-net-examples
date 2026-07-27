using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Assume the triangle is on the first page
                Page page = diagram.Pages[0];

                // Find the first shape whose master name is "Triangle"
                Shape? triangleShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        triangleShape = shape;
                        break;
                    }
                }

                if (triangleShape == null)
                {
                    throw new Exception("Triangle shape not found in the diagram.");
                }

                // Set fill transparency to 75% (0.75 = 75% transparent)
                // FillForegndTrans expects a value between 0.0 (opaque) and 1.0 (fully transparent)
                triangleShape.Fill.FillForegndTrans.Value = 0.75;

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }