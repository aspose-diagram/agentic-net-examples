using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the Visio file to process.
            string diagramPath;
            if (args.Length > 0 && File.Exists(args[0]))
            {
                diagramPath = args[0];
            }
            else
            {
                Console.WriteLine("Please enter the full path to the Visio file (.vsdx, .vsd, etc.):");
                diagramPath = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(diagramPath) || !File.Exists(diagramPath))
                {
                    Console.WriteLine("Invalid file path. Exiting.");
                    return;
                }
            }

            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Prepare CSV output.
            string csvPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "shapes_summary.csv");
            using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
            {
                // Write header.
                writer.WriteLine("ShapeID,Width,Height,ConnectionPoints");

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        long shapeId = shape.ID;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;
                        int connectionPoints = shape.Connections != null ? shape.Connections.Count : 0;

                        // Write CSV line.
                        writer.WriteLine($"{shapeId},{width},{height},{connectionPoints}");
                    }
                }
            }

            Console.WriteLine($"Shape summary CSV created at: {csvPath}");
        }
    }