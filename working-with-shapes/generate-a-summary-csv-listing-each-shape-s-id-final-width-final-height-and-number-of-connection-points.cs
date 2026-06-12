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
                // Output CSV file path
                string outputCsv = "summary.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,Width,Height,ConnectionPoints");

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve shape dimensions
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Count connection points (if collection is null, treat as zero)
                            int connectionCount = shape.Connections != null ? shape.Connections.Count : 0;

                            // Write CSV line
                            writer.WriteLine($"{shape.ID},{width},{height},{connectionCount}");
                        }
                    }
                }

                Console.WriteLine($"Summary CSV generated at: {Path.GetFullPath(outputCsv)}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }