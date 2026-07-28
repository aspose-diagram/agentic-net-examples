using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace OleSerializationExample
{
    // DTO for OLE object information
    public class OleInfo
    {
        public long ShapeId { get; set; }
        public string ShapeName { get; set; }
        public string ObjectSourceFullName { get; set; }
        public double ObjectWidth { get; set; }
        public double ObjectHeight { get; set; }
        public bool ShowAsIcon { get; set; }
        public string ForeignType { get; set; }
        public string ObjectType { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path (unchanged diagram saved back)
                string outputPath = "output.vsdx";
                // JSON output path
                string jsonPath = "oleInfo.json";

                // Load the diagram (create/load lifecycle)
                Diagram diagram = new Diagram(inputPath);

                List<OleInfo> oleInfos = new List<OleInfo>();

                // Iterate through pages and shapes to find OLE objects
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify shape is a foreign OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            var fd = shape.ForeignData;

                            // Ensure there is embedded OLE binary data
                            if (fd.ObjectData != null && fd.ObjectData.Length > 0)
                            {
                                OleInfo info = new OleInfo
                                {
                                    ShapeId = shape.ID,
                                    ShapeName = shape.Name,
                                    ObjectSourceFullName = fd.ObjectSourceFullName,
                                    ObjectWidth = fd.ObjectWidth,
                                    ObjectHeight = fd.ObjectHeight,
                                    ShowAsIcon = fd.ShowAsIcon == BOOL.True,
                                    ForeignType = fd.ForeignType.ToString(),
                                    ObjectType = fd.ObjectType.ToString()
                                };

                                oleInfos.Add(info);
                            }
                        }
                    }
                }

                // Serialize the collected OLE information to JSON
                string json = JsonSerializer.Serialize(oleInfos, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, json);
                Console.WriteLine($"OLE information serialized to {jsonPath}");

                // Save the (unchanged) diagram back to a file (save lifecycle)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}