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
        public string NameU { get; set; }
        public string ObjectSourceFullName { get; set; }
        public bool ShowAsIcon { get; set; }
        public double ObjectWidth { get; set; }
        public double ObjectHeight { get; set; }
        public string ObjectType { get; set; }
        public string Base64Data { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect OLE object information
                List<OleInfo> oleInfos = new List<OleInfo>();

                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectData.Length > 0)
                        {
                            OleInfo info = new OleInfo
                            {
                                ShapeId = shape.ID,
                                NameU = shape.NameU,
                                ObjectSourceFullName = shape.ForeignData.ObjectSourceFullName,
                                ShowAsIcon = shape.ForeignData.ShowAsIcon == BOOL.True,
                                ObjectWidth = shape.ForeignData.ObjectWidth,
                                ObjectHeight = shape.ForeignData.ObjectHeight,
                                ObjectType = shape.ForeignData.ObjectType.ToString(),
                                Base64Data = Convert.ToBase64String(shape.ForeignData.ObjectData)
                            };

                            oleInfos.Add(info);
                        }
                    }
                }

                // Serialize the collected information to JSON
                string json = JsonSerializer.Serialize(oleInfos, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to a file
                File.WriteAllText("ole_info.json", json);

                // Optionally save the diagram (unchanged) to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}