using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Assume the triangle shape is on the first page
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page to find the triangle master
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape uses the "Triangle" master
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        // Set a solid fill pattern (1 = solid)
                        shape.Fill.FillPattern.Value = 1;

                        // Set the foreground fill color to blue (hex code)
                        shape.Fill.FillForegnd.Value = "#0000FF";

                        // Optionally break after the first triangle is processed
                        break;
                    }
                }

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }