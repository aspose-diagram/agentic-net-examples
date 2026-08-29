using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioShapeAnglesExport <inputVisioPath> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Open CSV writer
            try
            {
                using (var writer = new StreamWriter(outputCsvPath))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,Angle");

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            long shapeId = shape.ID;
                            double angle = shape.XForm.Angle.Value; // Angle is stored in radians

                            // Write shape ID and angle to CSV
                            writer.WriteLine($"{shapeId},{angle}");
                        }
                    }
                }

                Console.WriteLine($"CSV file created successfully at: {outputCsvPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write CSV: {ex.Message}");
            }
        }
    }