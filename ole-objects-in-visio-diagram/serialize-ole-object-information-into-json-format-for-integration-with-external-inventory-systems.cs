using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace OleInfoExport
{
    // DTO for OLE object information
    public class OleObjectInfo
    {
        public long ShapeId { get; set; }
        public string ShapeName { get; set; }
        public string ObjectType { get; set; }
        public string ObjectSourceFullName { get; set; }
        public bool ShowAsIcon { get; set; }
        public double ObjectWidth { get; set; }
        public double ObjectHeight { get; set; }
        public string DataBase64 { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // List to hold OLE object information
                List<OleObjectInfo> oleInfos = new List<OleObjectInfo>();

                // Iterate through all pages
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Verify the shape is a foreign (OLE) shape
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                        {
                            // Verify the foreign data represents an OLE object
                            if (shape.ForeignData.ForeignType == ForeignType.Object)
                            {
                                // Ensure there is binary data to process
                                if (shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                                {
                                    OleObjectInfo info = new OleObjectInfo();

                                    // Basic shape identification
                                    info.ShapeId = shape.ID;
                                    info.ShapeName = shape.NameU;

                                    // OLE specific properties
                                    info.ObjectType = shape.ForeignData.ObjectType.ToString();
                                    info.ObjectSourceFullName = shape.ForeignData.ObjectSourceFullName;
                                    info.ShowAsIcon = shape.ForeignData.ShowAsIcon == BOOL.True;
                                    info.ObjectWidth = shape.ForeignData.ObjectWidth;
                                    info.ObjectHeight = shape.ForeignData.ObjectHeight;

                                    // Convert binary data to Base64 for JSON transport
                                    info.DataBase64 = Convert.ToBase64String(shape.ForeignData.ObjectData);

                                    oleInfos.Add(info);
                                }
                            }
                        }
                    }
                }

                // Serialize the list to JSON with indentation
                string json = JsonSerializer.Serialize(oleInfos, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file (replace with desired output path)
                File.WriteAllText("oleInfo.json", json);

                Console.WriteLine("OLE object information has been exported to oleInfo.json");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}