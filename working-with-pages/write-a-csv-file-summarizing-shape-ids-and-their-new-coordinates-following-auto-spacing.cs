using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output CSV file path
                string csvOutputPath = "shape_coordinates.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Configure auto-space options (default distances)
                    AutoSpaceOptions options = new AutoSpaceOptions
                    {
                        DistanceInHorizontal = 0.5, // inches
                        DistanceInVertical = 0.5    // inches
                    };

                    // Apply auto-spacing to the shapes on the page
                    page.AutoSpaceShapes(page.Shapes, options);
                }

                // Write shape IDs and new coordinates to CSV
                using (StreamWriter writer = new StreamWriter(csvOutputPath))
                {
                    // CSV header
                    writer.WriteLine("ShapeID,PinX,PinY");

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve shape ID and coordinates
                            long shapeId = shape.ID;
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;

                            // Write CSV line
                            writer.WriteLine($"{shapeId},{pinX},{pinY}");
                        }
                    }
                }

                // Optionally save the modified diagram (preserving changes)
                string outputDiagramPath = "output_auto_spaced.vsdx";
                diagram.Save(outputDiagramPath, SaveFileFormat.Csv);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }