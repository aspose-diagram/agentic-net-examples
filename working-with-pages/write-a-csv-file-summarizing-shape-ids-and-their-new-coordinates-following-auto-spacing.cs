using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output CSV file path
                string outputCsvPath = "shape_coordinates.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page for auto‑spacing
                Page page = diagram.Pages[0];

                // Configure auto‑spacing options (default distances can be overridden here)
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 1.0, // inches between shapes horizontally
                    DistanceInVertical = 1.0    // inches between shapes vertically
                };

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                // Write shape IDs and their new coordinates to a CSV file
                using (StreamWriter writer = new StreamWriter(outputCsvPath))
                {
                    // CSV header
                    writer.WriteLine("ShapeID,PinX,PinY");

                    // Iterate through shapes and output their coordinates
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        long shapeId = shape.ID;
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        writer.WriteLine($"{shapeId},{pinX},{pinY}");
                    }
                }

                Console.WriteLine($"CSV file generated at: {Path.GetFullPath(outputCsvPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }