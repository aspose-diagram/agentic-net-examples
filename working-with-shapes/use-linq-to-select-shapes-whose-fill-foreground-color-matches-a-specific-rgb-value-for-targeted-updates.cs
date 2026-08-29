using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Define the target fill foreground color (hex string) and the new color
                string targetColor = "#FF0000"; // Red
                string newColor = "#00FF00";    // Green

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Select all shapes across all pages whose fill foreground color matches the target color
                var matchingShapes = diagram.Pages
                    .SelectMany(page => page.Shapes.Cast<Shape>())
                    .Where(shape => shape.Fill.FillForegnd.Value.Equals(targetColor, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Update the fill foreground color for each matching shape
                foreach (var shape in matchingShapes)
                {
                    shape.Fill.FillForegnd.Value = newColor;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Updated {matchingShapes.Count} shape(s) from {targetColor} to {newColor}.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }