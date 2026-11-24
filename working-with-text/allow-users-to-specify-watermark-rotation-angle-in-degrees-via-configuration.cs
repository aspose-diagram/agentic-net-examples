using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, configuration file path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <configPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string configPath = args[2];

        // Guard against missing files
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read rotation angle from configuration (JSON format: { "RotationAngle": 45 })
        double rotationAngleDeg;
        try
        {
            string json = File.ReadAllText(configPath);
            using JsonDocument doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("RotationAngle", out JsonElement angleElem) ||
                !angleElem.TryGetDouble(out rotationAngleDeg))
            {
                Console.Error.WriteLine("Configuration file must contain a numeric \"RotationAngle\" property.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by Aspose)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page; create a new one if necessary
                Page page;
                if (diagram.Pages.Count == 0)
                {
                    // Determine a new unique page ID
                    int maxId = 0;
                    foreach (Page p in diagram.Pages)
                    {
                        if (p.ID > maxId) maxId = p.ID;
                    }
                    Page newPage = new Page(maxId + 1);
                    diagram.Pages.Add(newPage);
                    page = newPage;
                }
                else
                {
                    page = diagram.Pages[0];
                }

                // Add a rectangle shape that will serve as the watermark
                // Parameters: pinX, pinY, width, height (in inches)
                double pinX = 2.0;
                double pinY = 2.0;
                double width = 5.0;
                double height = 5.0;
                long shapeId = page.DrawRectangle(pinX, pinY, width, height);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Clear any existing text and set watermark text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("CONFIDENTIAL"));

                // Apply rotation (degrees) to the shape
                shape.SetAngle(rotationAngleDeg);

                // Make the shape semi‑transparent (0 = opaque, 1 = fully transparent)
                shape.Fill.FillForegndTrans.Value = 0.8;

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with watermark rotation {rotationAngleDeg}° to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}