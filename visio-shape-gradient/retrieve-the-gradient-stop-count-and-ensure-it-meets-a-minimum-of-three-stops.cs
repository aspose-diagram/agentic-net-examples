using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Access the gradient fill of the shape
                        GradientFill gradientFill = shape.Fill.GradientFill;
                        if (gradientFill == null)
                            continue;

                        // Retrieve the current number of gradient stops
                        int stopCount = gradientFill.GradientStops.Count;
                        Console.WriteLine($"Shape ID {shape.ID} has {stopCount} gradient stop(s).");

                        // Ensure there are at least three stops
                        if (stopCount < 3)
                        {
                            // Clear existing stops and add three default stops
                            gradientFill.GradientStops.Clear();

                            // Position 0 (start) – red
                            gradientFill.GradientStops.Add(
                                new DoubleValue(0, MeasureConst.NUM),
                                new ColorValue("#FF0000", MeasureConst.Undefined));

                            // Position 0.5 (middle) – green
                            gradientFill.GradientStops.Add(
                                new DoubleValue(0.5, MeasureConst.NUM),
                                new ColorValue("#00FF00", MeasureConst.Undefined));

                            // Position 1 (end) – blue
                            gradientFill.GradientStops.Add(
                                new DoubleValue(1, MeasureConst.NUM),
                                new ColorValue("#0000FF", MeasureConst.Undefined));

                            Console.WriteLine($"Added 3 gradient stops to shape ID {shape.ID}.");
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }