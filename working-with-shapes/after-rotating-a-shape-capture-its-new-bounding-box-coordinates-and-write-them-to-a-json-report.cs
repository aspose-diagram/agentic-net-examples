using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file and output JSON report paths
            string diagramPath = "input.vsdx";
            string jsonReportPath = "report.json";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Select the page and shape to rotate (example: first shape on the first page)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0]; // replace with specific shape selection as needed

            // Desired rotation angle in degrees
            double rotationAngle = 45.0;

            // Apply rotation
            shape.XForm.Angle.Value = rotationAngle;

            // Refresh shape data so geometry reflects the new rotation
            shape.RefreshData();

            // Retrieve updated geometry values
            double pinX = shape.XForm.PinX.Value;      // center X
            double pinY = shape.XForm.PinY.Value;      // center Y
            double width = shape.XForm.Width.Value;
            double height = shape.XForm.Height.Value;

            // Calculate bounding box coordinates (left, top, right, bottom)
            double left = pinX - width / 2.0;
            double top = pinY - height / 2.0;
            double right = left + width;
            double bottom = top + height;

            // Build a report object
            var report = new
            {
                ShapeId = shape.ID,
                RotationAngle = rotationAngle,
                BoundingBox = new
                {
                    Left = left,
                    Top = top,
                    Right = right,
                    Bottom = bottom
                }
            };

            // Serialize the report to formatted JSON
            string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON report to a file
            File.WriteAllText(jsonReportPath, json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
