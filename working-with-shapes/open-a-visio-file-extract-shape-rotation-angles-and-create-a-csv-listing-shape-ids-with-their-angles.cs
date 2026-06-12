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
                string visioPath = "input.vsdx";

                // Output CSV file path
                string csvPath = "shape_rotations.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Prepare to write CSV
                using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,AngleDegrees");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve shape ID
                            long shapeId = shape.ID;

                            // Retrieve rotation angle (in radians) and convert to degrees
                            double angleRadians = shape.XForm.Angle.Value;
                            double angleDegrees = angleRadians * 180.0 / Math.PI;

                            // Write to CSV
                            writer.WriteLine($"{shapeId},{angleDegrees}");
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