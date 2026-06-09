using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace VisioWebApi
{
    public class ModifyRequest
    {
        public long ShapeId { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double AngleDeg { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.Error.WriteLine("Usage: <inputDiagramPath> <outputDiagramPath> <requestJsonPath>");
                return;
            }

            string inputPath = args[0];
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            string outputPath = args[1];
            string requestJsonPath = args[2];
            if (!File.Exists(requestJsonPath))
            {
                Console.Error.WriteLine($"File not found: {requestJsonPath}");
                return;
            }

            ModifyRequest request;
            try
            {
                string json = File.ReadAllText(requestJsonPath);
                request = JsonSerializer.Deserialize<ModifyRequest>(json);
                if (request == null)
                {
                    Console.Error.WriteLine("Invalid request payload.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read or deserialize request: {ex.Message}");
                return;
            }

            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Diagram contains no pages.");
                return;
            }

            var page = diagram.Pages[0];
            Shape shape;
            try
            {
                shape = page.Shapes.GetShape(request.ShapeId);
            }
            catch (Exception)
            {
                Console.Error.WriteLine($"Shape with ID {request.ShapeId} not found.");
                return;
            }

            // Update position
            shape.XForm.PinX.Value = request.PinX;
            shape.XForm.PinY.Value = request.PinY;

            // Update rotation (Angle expects radians)
            double angleRad = request.AngleDeg * Math.PI / 180.0;
            shape.XForm.Angle.Value = angleRad;

            // Example geometry manipulation: add a new vertex to the first geometry path
            if (shape.Geoms.Count > 0)
            {
                var geom = shape.Geoms[0];
                var lineTo = new LineTo();
                lineTo.X.Value = shape.XForm.PinX.Value + 0.5;
                lineTo.Y.Value = shape.XForm.PinY.Value;
                geom.CoordinateCol.Add(lineTo);
            }

            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
                return;
            }

            Console.WriteLine("Shape geometry updated successfully.");
        }
    }
}