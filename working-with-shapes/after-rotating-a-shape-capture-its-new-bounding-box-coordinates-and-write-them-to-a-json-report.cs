using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output JSON report path
        string jsonReportPath = "bounding_box_report.json";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("Error: The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // Rotate the shape to a desired angle (e.g., 45 degrees)
                double newAngle = 45.0;
                shape.XForm.Angle.Value = newAngle;

                // After rotation, capture the bounding box coordinates
                // PinX and PinY represent the center of the shape
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;
                double width = shape.XForm.Width.Value;
                double height = shape.XForm.Height.Value;

                // Calculate left, right, top, bottom (in inches)
                double left = pinX - (width / 2.0);
                double right = pinX + (width / 2.0);
                double top = pinY + (height / 2.0);
                double bottom = pinY - (height / 2.0);

                // Prepare a report object
                var report = new
                {
                    ShapeId = shape.ID,
                    RotationAngle = newAngle,
                    BoundingBox = new
                    {
                        Left = left,
                        Top = top,
                        Right = right,
                        Bottom = bottom,
                        Width = width,
                        Height = height
                    }
                };

                // Serialize the report to JSON with indentation
                string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON report to the specified file
                File.WriteAllText(jsonReportPath, json);
                Console.WriteLine($"Bounding box report written to '{jsonReportPath}'.");

                // Optional: save the modified diagram to a new file
                string outputDiagramPath = "rotated_output.vsdx";
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Rotated diagram saved to '{outputDiagramPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}