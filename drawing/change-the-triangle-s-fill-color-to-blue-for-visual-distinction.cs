using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages and shapes to find the triangle shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a master and that the master name is "Triangle"
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            // Set solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // 1 = solid

                            // Change fill color to blue (hex code)
                            shape.Fill.FillForegnd.Value = "#0000FF";
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