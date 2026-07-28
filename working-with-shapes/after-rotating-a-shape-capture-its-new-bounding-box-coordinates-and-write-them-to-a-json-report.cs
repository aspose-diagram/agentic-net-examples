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

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first non-deleted shape on the first page
                Page page = diagram.Pages[0];
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("No suitable shape found to rotate.");
                }

                // Rotate the shape (angle in degrees)
                double newAngleDegrees = 45.0;
                targetShape.XForm.Angle.Value = newAngleDegrees;

                // Refresh shape data to ensure geometry is updated
                targetShape.RefreshData();

                // Calculate bounding box coordinates
                double pinX = targetShape.XForm.PinX.Value;
                double pinY = targetShape.XForm.PinY.Value;
                double width = targetShape.XForm.Width.Value;
                double height = targetShape.XForm.Height.Value;

                double left = pinX - width / 2.0;
                double right = pinX + width / 2.0;
                double top = pinY + height / 2.0;
                double bottom = pinY - height / 2.0;

                // Prepare JSON report
                var report = new
                {
                    ShapeId = targetShape.ID,
                    BoundingBox = new
                    {
                        Left = left,
                        Right = right,
                        Top = top,
                        Bottom = bottom
                    },
                    RotationDegrees = newAngleDegrees
                };

                string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("bounding_box_report.json", json);

                // Optionally save the modified diagram
                diagram.Save("rotated_output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }