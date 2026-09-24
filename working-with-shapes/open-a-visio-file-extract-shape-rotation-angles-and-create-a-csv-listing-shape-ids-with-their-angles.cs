using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output CSV file path
                string csvPath = "shape_angles.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare the CSV file
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,Angle");

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve shape ID and rotation angle (in degrees)
                            long shapeId = shape.ID;
                            double angle = shape.XForm.Angle.Value;

                            // Write to CSV
                            writer.WriteLine($"{shapeId},{angle}");
                        }
                    }
                }

                Console.WriteLine($"Shape rotation angles have been exported to '{csvPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }