using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <areaThreshold> [outputCsvPath]");
            return;
        }

        string inputPath = args[0];
        string thresholdArg = args[1];
        string outputPath = args.Length >= 3 ? args[2] : "ShapeAreaReport.csv";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse the area threshold.
        if (!double.TryParse(thresholdArg, out double areaThreshold))
        {
            Console.Error.WriteLine($"Error: Unable to parse area threshold \"{thresholdArg}\" as a number.");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        try
        {
            using (var writer = new StreamWriter(outputPath))
            {
                // Write CSV header.
                writer.WriteLine("ShapeId,ShapeName,Area");

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve width and height (in drawing units).
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;
                        double area = width * height;

                        // If the shape's area exceeds the threshold, write a CSV line.
                        if (area > areaThreshold)
                        {
                            // Shape ID is a long, Shape Name is a string.
                            string shapeId = shape.ID.ToString();
                            string shapeName = shape.Name ?? string.Empty;
                            writer.WriteLine($"{shapeId},{shapeName},{area}");
                        }
                    }
                }
            }

            Console.WriteLine($"Report generated successfully at \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error generating CSV report: {ex.Message}");
        }
        finally
        {
            // Dispose the diagram if it implements IDisposable.
            if (diagram is IDisposable disposableDiagram)
                disposableDiagram.Dispose();
        }
    }
}