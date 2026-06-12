using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                int redLineShapeCount = 0;

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape's line color is red (#FF0000)
                        if (shape.Line != null && shape.Line.LineColor != null &&
                            string.Equals(shape.Line.LineColor.Value, "#FF0000", StringComparison.OrdinalIgnoreCase))
                        {
                            redLineShapeCount++;
                        }
                    }
                }

                // Output the result
                Console.WriteLine($"Number of shapes with a red line: {redLineShapeCount}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }