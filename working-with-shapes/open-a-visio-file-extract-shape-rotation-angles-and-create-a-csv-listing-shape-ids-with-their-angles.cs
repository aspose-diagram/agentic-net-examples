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
                Console.WriteLine("Usage: VisioShapeRotationExtractor <inputVisioPath> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare to write CSV
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,AngleDegrees");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the shape's unique ID
                        long shapeId = shape.ID;

                        // Retrieve the rotation angle (stored in radians) and convert to degrees
                        double angleRadians = shape.XForm.Angle.Value;
                        double angleDegrees = angleRadians * (180.0 / Math.PI);

                        // Write the ID and angle to the CSV
                        writer.WriteLine($"{shapeId},{angleDegrees}");
                    }
                }
            }

            Console.WriteLine($"Shape rotation angles have been exported to '{outputPath}'.");
        }
    }