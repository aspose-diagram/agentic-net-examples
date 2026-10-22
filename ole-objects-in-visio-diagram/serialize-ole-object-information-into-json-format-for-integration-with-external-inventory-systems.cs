using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Model class representing OLE object information for JSON serialization
    public class OleObjectInfo
    {
        public int PageIndex { get; set; }
        public long ShapeId { get; set; }
        public string ShapeName { get; set; }
        public string ObjectSourceFullName { get; set; }
        public string ObjectDataBase64 { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string visioPath = args[0];
        string jsonPath = args[1];

        // Guard: verify input Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        // Guard: ensure the directory for the JSON output exists
        string jsonDir = Path.GetDirectoryName(jsonPath);
        if (!string.IsNullOrEmpty(jsonDir) && !Directory.Exists(jsonDir))
        {
            Console.Error.WriteLine($"Error: Output directory does not exist: '{jsonDir}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // List to hold extracted OLE object information
            List<OleObjectInfo> oleObjects = new List<OleObjectInfo>();

            // Iterate over pages with explicit index
            for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
            {
                Aspose.Diagram.Page page = diagram.Pages[pageIdx];

                // Iterate over shapes on the page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape contains ForeignData (potential OLE object)
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                    {
                        // Verify the object type is an embedded OLE object
                        if (shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                        {
                            OleObjectInfo info = new OleObjectInfo
                            {
                                PageIndex = pageIdx,
                                ShapeId = shape.ID,
                                ShapeName = shape.NameU,
                                ObjectSourceFullName = shape.ForeignData.ObjectSourceFullName,
                                ObjectDataBase64 = Convert.ToBase64String(shape.ForeignData.ObjectData)
                            };

                            oleObjects.Add(info);
                        }
                    }
                }
            }

            // Serialize the list to JSON with indentation
            string json = JsonSerializer.Serialize(oleObjects, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the specified output file
            File.WriteAllText(jsonPath, json);

            Console.WriteLine($"Successfully extracted {oleObjects.Count} OLE object(s) to '{jsonPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}