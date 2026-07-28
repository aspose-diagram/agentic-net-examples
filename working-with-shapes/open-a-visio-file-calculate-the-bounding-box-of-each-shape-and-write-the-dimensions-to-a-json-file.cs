using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace VisioBoundingBoxExporter
{
    // Represents the bounding box information for a shape.
    public class ShapeBoundingBox
    {
        public string PageName { get; set; }
        public long ShapeId { get; set; }
        public string ShapeName { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output JSON file path
                string outputPath = "shape_bounding_boxes.json";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // List to hold bounding box data for all shapes
                List<ShapeBoundingBox> boxes = new List<ShapeBoundingBox>();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve width and height from the shape's XForm cell collection
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Retrieve the pin (center) coordinates
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Create a bounding box record
                        ShapeBoundingBox box = new ShapeBoundingBox
                        {
                            PageName = page.Name,
                            ShapeId = shape.ID,
                            ShapeName = shape.Name,
                            Width = width,
                            Height = height,
                            PinX = pinX,
                            PinY = pinY
                        };

                        boxes.Add(box);
                    }
                }

                // Serialize the list to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(boxes, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Bounding box data for {boxes.Count} shapes written to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}