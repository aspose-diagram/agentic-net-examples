using System;
using System.Linq;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the target fill foreground color (hex format)
                string targetColor = "#FF0000"; // Red

                // Find all non‑deleted shapes whose FillForegnd matches the target color
                List<Shape> matchingShapes = diagram.Pages
                    .Cast<Page>()
                    .SelectMany(page => page.Shapes.Cast<Shape>())
                    .Where(shape =>
                        shape.Del == BOOL.False &&                                   // Exclude deleted shapes
                        string.Equals(shape.Fill.FillForegnd.Value, targetColor, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Apply the desired update – change the fill foreground color to green
                foreach (Shape shape in matchingShapes)
                {
                    shape.Fill.FillForegnd.Value = "#00FF00"; // Green
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Updated {matchingShapes.Count} shape(s) and saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }