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

                // Iterate through all pages and shapes to find the circle shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify the shape by its universal name containing "Circle"
                        if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("Circle"))
                        {
                            // Set fill color to blue (hex format)
                            shape.Fill.FillForegnd.Value = "#0000FF";

                            // Set line weight to 0.5 points (Visio uses inches for line weight)
                            // 1 point = 1/72 inch
                            shape.Line.LineWeight.Value = 0.5 / 72.0;
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