using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and filter shapes that inherit both fill and line formatting
                foreach (Page page in diagram.Pages)
                {
                    var inheritedShapes = page.Shapes
                        .Cast<Shape>()
                        .Where(shape =>
                            // Exclude deleted shapes
                            shape.Del == BOOL.False &&
                            // Fill inheritance: foreground color and pattern match inherited values
                            shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value &&
                            shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value &&
                            // Line inheritance: line color and pattern match inherited values
                            shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value &&
                            shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value)
                        .ToList();

                    // Output information about the filtered shapes
                    Console.WriteLine($"Page \"{page.Name}\" contains {inheritedShapes.Count} shape(s) inheriting both fill and line formatting:");
                    foreach (Shape shape in inheritedShapes)
                    {
                        Console.WriteLine($"  Shape ID: {shape.ID}, NameU: {shape.NameU}");
                    }
                }

                // Optional: keep console window open when running outside debugger
                Console.WriteLine("Processing complete. Press any key to exit.");
                Console.ReadKey();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }