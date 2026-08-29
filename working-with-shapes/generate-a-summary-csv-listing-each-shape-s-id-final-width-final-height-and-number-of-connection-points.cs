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
            string outputPath = args[1];

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

            // Create or overwrite the CSV file
            try
            {
                using (var writer = new StreamWriter(outputPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,Width,Height,ConnectionPoints");

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            long shapeId = shape.ID;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;
                            int connectionCount = shape.Connections.Count;

                            // Write CSV line
                            writer.WriteLine($"{shapeId},{width},{height},{connectionCount}");
                        }
                    }
                }

                Console.WriteLine($"CSV summary successfully written to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write CSV: {ex.Message}");
            }
        }
    }