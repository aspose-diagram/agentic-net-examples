using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, config file, output Visio file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <configPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard against missing configuration file
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read solid color from configuration (expected as a hex string, e.g., "#FF0000")
        string solidColor = File.ReadAllText(configPath).Trim();
        if (string.IsNullOrEmpty(solidColor) || !solidColor.StartsWith("#"))
        {
            Console.Error.WriteLine("Configuration file must contain a valid hex color string starting with '#'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Fill and GradientFill defined
                        if (shape.Fill != null &&
                            shape.Fill.GradientFill != null &&
                            shape.Fill.GradientFill.GradientEnabled != null &&
                            shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                        {
                            // Replace gradient with a solid fill
                            // Set fill pattern to solid (value 1)
                            shape.Fill.FillPattern.Value = 1;

                            // Disable gradient
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False;

                            // Clear existing gradient stops
                            shape.Fill.GradientFill.GradientStops.Clear();

                            // Add a single stop with the solid color from configuration
                            shape.Fill.GradientFill.GradientStops.Add(
                                new DoubleValue(0, MeasureConst.NUM),
                                new ColorValue(solidColor, MeasureConst.Undefined));
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}