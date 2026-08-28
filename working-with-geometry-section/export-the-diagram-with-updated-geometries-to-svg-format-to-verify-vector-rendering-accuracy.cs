using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output SVG file path
                string outputPath = "output.svg";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Access the first page explicitly
                Page page = diagram.Pages[0];

                // Attempt to retrieve a shape with ID 1 (ensure it exists)
                Shape shape = page.Shapes.GetShape(1);
                if (shape != null)
                {
                    // Ensure the shape has at least one geometry section
                    if (shape.Geoms.Count > 0)
                    {
                        // Cast the first geometry to Geom
                        Geom geom = (Geom)shape.Geoms[0];

                        // Create a MoveTo segment at (0,0)
                        MoveTo move = new MoveTo();
                        move.X.Value = 0.0;
                        move.Y.Value = 0.0;
                        geom.CoordinateCol.Add(move);

                        // Create a LineTo segment to (1,1)
                        LineTo line = new LineTo();
                        line.X.Value = 1.0;
                        line.Y.Value = 1.0;
                        geom.CoordinateCol.Add(line);

                        Console.WriteLine("Geometry of shape ID 1 has been updated.");
                    }
                    else
                    {
                        Console.WriteLine("Shape ID 1 does not contain any geometry sections.");
                    }
                }
                else
                {
                    Console.WriteLine("Shape with ID 1 was not found on the first page.");
                }

                // Configure SVG save options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportHiddenPage = false,
                    ExportGuideShapes = false,
                    SVGFitToViewPort = true,
                    ExportElementAsRectTag = true
                };

                // Save the diagram as SVG using the configured options
                diagram.Save(outputPath, svgOptions);

                Console.WriteLine($"Diagram exported to SVG successfully: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }