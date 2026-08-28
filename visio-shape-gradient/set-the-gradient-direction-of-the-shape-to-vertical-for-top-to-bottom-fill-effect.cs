using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page (adjust index as needed)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (replace 1 with the actual shape ID you want to modify)
                Shape shape = page.Shapes.GetShape(1);

                // Enable gradient fill
                shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Set gradient direction to vertical (top‑to‑bottom)
                // 0 = Left‑to‑Right, 1 = Top‑to‑Bottom, 2 = Diagonal (based on Visio conventions)
                shape.Fill.GradientFill.GradientDir.Value = 1;

                // Clear any existing gradient stops
                shape.Fill.GradientFill.GradientStops.Clear();

                // Add gradient stops (example: red at the top, green at the bottom)
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),               // Position at start (0%)
                    new ColorValue("#FF0000", MeasureConst.Undefined)); // Red color

                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),               // Position at end (100%)
                    new ColorValue("#00FF00", MeasureConst.Undefined)); // Green color

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }