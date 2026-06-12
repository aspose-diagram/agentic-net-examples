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

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // Rotate the shape by 45 degrees (as per rule, SetAngle uses degrees)
                shape.SetAngle(45);

                // Capture the new bounding box coordinates and size
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;
                double width = shape.XForm.Width.Value;
                double height = shape.XForm.Height.Value;
                double angle = shape.XForm.Angle.Value; // angle in radians after rotation

                // Prepare a simple report object
                var report = new
                {
                    ShapeId = shape.ID,
                    PinX = pinX,
                    PinY = pinY,
                    Width = width,
                    Height = height,
                    AngleRadians = angle
                };

                // Serialize the report to JSON with indentation
                string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });

                // Write the JSON report to a file
                string outputReportPath = "shape_report.json";
                File.WriteAllText(outputReportPath, json);

                // Optionally, save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }