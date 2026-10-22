using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, config file path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <configPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string configPath = args[2];

        // Guard against missing files
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read rotation angle from configuration (expects a line like "RotationAngle=45")
        double rotationAngleDeg = 0;
        try
        {
            foreach (var line in File.ReadAllLines(configPath))
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                    continue;

                var parts = line.Split(new[] { '=' }, 2);
                if (parts[0].Trim().Equals("RotationAngle", StringComparison.OrdinalIgnoreCase))
                {
                    if (double.TryParse(parts[1].Trim(), out double parsed))
                    {
                        rotationAngleDeg = parsed;
                    }
                    else
                    {
                        Console.Error.WriteLine($"Invalid rotation angle value in config: {parts[1]}");
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        // Load the diagram and apply watermark
        try
        {
            // Use a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page for the watermark
                Page page = diagram.Pages[0];

                // Define rectangle dimensions (in inches)
                double pinX = 2.0;
                double pinY = 2.0;
                double width = 5.0;
                double height = 2.0;

                // Draw a rectangle shape that will act as the watermark
                long shapeId = page.DrawRectangle(pinX, pinY, width, height);
                Shape watermarkShape = page.Shapes.GetShape(shapeId);

                // Clear any existing text and add watermark text
                watermarkShape.Text.Value.Clear();
                watermarkShape.Text.Value.Add(new Txt("CONFIDENTIAL"));

                // Set a light gray fill color
                watermarkShape.Fill.FillForegnd.Value = "#D3D3D3";

                // Rotate the shape by the configured angle (degrees)
                watermarkShape.SetAngle(rotationAngleDeg);

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}