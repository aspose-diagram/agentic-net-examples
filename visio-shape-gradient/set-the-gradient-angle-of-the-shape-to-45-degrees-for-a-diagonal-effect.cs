using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape shape = null;
                foreach (Shape s in page.Shapes)
                {
                    shape = s;
                    break;
                }

                if (shape == null)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                // Enable gradient fill
                shape.Fill.FillPattern.Value = 25; // 25 corresponds to gradient fill pattern
                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                // Set the gradient angle to 45 degrees for a diagonal effect
                shape.Fill.GradientFill.GradientAngle.Value = 45;

                // Optional: define gradient stops (blue to green) – can be adjusted as needed
                shape.Fill.GradientFill.GradientStops.Clear();
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(0, MeasureConst.NUM),
                    new ColorValue("#0000FF", MeasureConst.Undefined));
                shape.Fill.GradientFill.GradientStops.Add(
                    new DoubleValue(1, MeasureConst.NUM),
                    new ColorValue("#00FF00", MeasureConst.Undefined));

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved with gradient angle set to 45 degrees.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }