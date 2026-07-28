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
                Console.WriteLine("Usage: DiagramSummaryCsv <inputVisioFile> <outputCsvFile>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create or overwrite the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("ShapeID,Width,Height,ConnectionPoints");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        long shapeId = shape.ID;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;
                        int connectionPoints = shape.Connections.Count;

                        // Write a CSV line for the current shape
                        writer.WriteLine($"{shapeId},{width},{height},{connectionPoints}");
                    }
                }
            }

            Console.WriteLine($"CSV summary has been written to: {outputCsvPath}");
        }
    }