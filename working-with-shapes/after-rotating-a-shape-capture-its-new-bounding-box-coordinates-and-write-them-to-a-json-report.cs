using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramBoundingBoxReport
{
    // DTO for JSON serialization
    public class ShapeBoundingBox
    {
        public long ShapeId { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Angle { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
        public double Top { get; set; }
        public double Bottom { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output JSON report path
                string jsonReportPath = "BoundingBoxReport.json";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                Page page = diagram.Pages[0];

                if (page.Shapes.Count == 0)
                {
                    throw new Exception("The first page contains no shapes.");
                }

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // Rotate the shape to a new angle (degrees)
                double newAngle = 45.0;
                shape.XForm.Angle.Value = newAngle;

                // Capture bounding box after rotation
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;
                double width = shape.XForm.Width.Value;
                double height = shape.XForm.Height.Value;

                // Calculate bounding box edges (Visio uses center coordinates)
                double left = pinX - (width / 2.0);
                double right = pinX + (width / 2.0);
                double top = pinY + (height / 2.0);
                double bottom = pinY - (height / 2.0);

                // Prepare DTO
                ShapeBoundingBox bbox = new ShapeBoundingBox
                {
                    ShapeId = shape.ID,
                    PinX = pinX,
                    PinY = pinY,
                    Width = width,
                    Height = height,
                    Angle = newAngle,
                    Left = left,
                    Right = right,
                    Top = top,
                    Bottom = bottom
                };

                // Serialize to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(bbox, jsonOptions);

                // Write JSON report to file
                File.WriteAllText(jsonReportPath, json);

                // Optionally, save the modified diagram (e.g., to a new file)
                string outputDiagramPath = "output_modified.vsdx";
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}