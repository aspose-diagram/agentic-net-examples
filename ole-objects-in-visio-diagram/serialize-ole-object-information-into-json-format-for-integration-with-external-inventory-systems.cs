using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace OLEObjectExporter
{
    // DTO representing OLE object information for JSON serialization
    public class OleInfo
    {
        public long ShapeId { get; set; }
        public string ShapeName { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool ShowAsIcon { get; set; }
        public string ObjectDataBase64 { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output JSON file path
                string outputPath = "ole_objects.json";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // List to hold extracted OLE information
                List<OleInfo> oleInfos = new List<OleInfo>();

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Extract required properties
                            long shapeId = shape.ID;
                            string shapeName = shape.Name;
                            double width = shape.ForeignData.ObjectWidth;
                            double height = shape.ForeignData.ObjectHeight;
                            bool showAsIcon = shape.ForeignData.ShowAsIcon == BOOL.True;

                            // Convert binary OLE data to Base64 (if present)
                            string base64Data = null;
                            if (shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                            {
                                base64Data = Convert.ToBase64String(shape.ForeignData.ObjectData);
                            }

                            // Populate DTO and add to list
                            OleInfo info = new OleInfo
                            {
                                ShapeId = shapeId,
                                ShapeName = shapeName,
                                Width = width,
                                Height = height,
                                ShowAsIcon = showAsIcon,
                                ObjectDataBase64 = base64Data
                            };
                            oleInfos.Add(info);
                        }
                    }
                }

                // Serialize the list to JSON with indentation
                string json = JsonSerializer.Serialize(oleInfos, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to output file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"OLE object information exported to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}