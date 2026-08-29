using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and configuration file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: GradientToSolid <inputVisioPath> <configPath>");
                return;
            }

            string inputVisioPath = args[0];
            string configPath = args[1];
            string outputVisioPath = Path.Combine(
                Path.GetDirectoryName(inputVisioPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputVisioPath) + "_solid.vsdx");

            // Read the solid fill color from the configuration file.
            // The file should contain a single hex color string, e.g., "#FF0000".
            string solidColor = File.ReadAllText(configPath).Trim();

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputVisioPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape uses a gradient fill (FillPattern = 25).
                    if (shape.Fill.FillPattern.Value == 25)
                    {
                        // Replace gradient with solid fill.
                        shape.Fill.FillPattern.Value = 1; // Solid fill pattern.
                        shape.Fill.FillForegnd.Value = solidColor; // Set foreground color.

                        // Disable gradient and clear any existing gradient stops.
                        shape.Fill.GradientFill.GradientEnabled.Value = BOOL.False;
                        shape.Fill.GradientFill.GradientStops.Clear();
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with solid fills to: {outputVisioPath}");
        }
    }