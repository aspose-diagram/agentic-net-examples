using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument or default)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                // Output CSV file path (second argument or default)
                string outputPath = args.Length > 1 ? args[1] : "shapes.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare CSV content
                using (var writer = new StreamWriter(outputPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,ShapeType,AreaSqMm");

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve shape ID
                            long shapeId = shape.ID;

                            // Retrieve shape type as string
                            string shapeType = shape.Type.ToString();

                            // Width and height are in inches; convert to millimeters (1 inch = 25.4 mm)
                            double widthInches = shape.XForm.Width.Value;
                            double heightInches = shape.XForm.Height.Value;
                            double areaSqMm = widthInches * heightInches * 25.4 * 25.4;

                            // Write CSV line
                            writer.WriteLine($"{shapeId},{shapeType},{areaSqMm:F2}");
                        }
                    }
                }

                Console.WriteLine($"CSV export completed: {outputPath}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }