using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the uniform gray color in hexadecimal
                const string grayHex = "#808080";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape uses a gradient fill (FillPattern value 25)
                        if (shape.Fill.FillPattern.Value == 25)
                        {
                            // Ensure gradient is enabled
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                            // Clear any existing gradient stops
                            shape.Fill.GradientFill.GradientStops.Clear();

                            // Add a start stop at position 0 with gray color
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(0, MeasureConst.NUM),
                                new ColorValue(grayHex, MeasureConst.Undefined));

                            // Add an end stop at position 1 with gray color
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(1, MeasureConst.NUM),
                                new ColorValue(grayHex, MeasureConst.Undefined));
                        }
                    }
                }

                // Save the modified diagram (replace with your desired output path)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }