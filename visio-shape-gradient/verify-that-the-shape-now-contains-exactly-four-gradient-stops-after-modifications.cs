using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Assume the target shape is on the first page with ID = 1
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(1);

                // Apply gradient fill with exactly four stops
                shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                shape.Fill.GradientFill.GradientDir.Value = 0; // Direction (optional)
                shape.Fill.GradientFill.GradientStops.Clear();

                // Add four gradient stops
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#FF0000", MeasureConst.Undefined)); // Red at start

                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0.33, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined)); // Green

                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0.66, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined)); // Blue

                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#FFFF00", MeasureConst.Undefined)); // Yellow at end

                // Verify that the shape now contains exactly four gradient stops
                int stopCount = shape.Fill.GradientFill.GradientStops.Count;
                if (stopCount != 4)
                {
                    throw new Exception($"Gradient stop verification failed. Expected 4 stops, but found {stopCount}.");
                }
                else
                {
                    Console.WriteLine("Gradient stop verification succeeded. Shape contains exactly four gradient stops.");
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }