using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, configuration file, output Visio file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <configPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard: verify input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputPath}");
            return;
        }

        // Guard: verify configuration file exists
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read the solid color (hex string) from the configuration file (first non‑empty line)
        string solidColor = null;
        try
        {
            foreach (string line in File.ReadAllLines(configPath))
            {
                string trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    solidColor = trimmed;
                    break;
                }
            }

            if (string.IsNullOrEmpty(solidColor))
            {
                Console.Error.WriteLine("Configuration file does not contain a color value.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration file: {ex.Message}");
            return;
        }

        // Load the Visio diagram and replace gradient fills with the solid color
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Detect gradient fill by checking the FillPattern value (25 indicates gradient)
                        if (shape.Fill != null &&
                            shape.Fill.FillPattern != null &&
                            shape.Fill.FillPattern.Value == 25)
                        {
                            // Disable the gradient fill if possible
                            if (shape.Fill.GradientFill != null &&
                                shape.Fill.GradientFill.GradientEnabled != null)
                            {
                                shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False;
                            }

                            // Clear any existing gradient stops
                            if (shape.Fill.GradientFill != null &&
                                shape.Fill.GradientFill.GradientStops != null)
                            {
                                shape.Fill.GradientFill.GradientStops.Clear();
                            }

                            // Set solid fill pattern (1 = solid)
                            shape.Fill.FillPattern.Value = 1;

                            // Apply the solid color from the configuration file
                            shape.Fill.FillForegnd.Value = solidColor;
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}