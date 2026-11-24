using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputJsonFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare a list to hold OLE object information.
            List<OleInfo> oleInfos = new List<OleInfo>();

            // Iterate through each page in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Aspose.Diagram.Page page = diagram.Pages[pageIndex];

                // Iterate through each shape on the page.
                for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                {
                    Aspose.Diagram.Shape shape = page.Shapes[shapeIdx];

                    // Detect OLE (foreign) objects via ForeignData.
                    if (shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                    {
                        // Gather relevant OLE details.
                        OleInfo info = new OleInfo
                        {
                            PageIndex = pageIndex,
                            ShapeId = shape.ID,
                            ObjectType = shape.ForeignData.ObjectType.ToString(),
                            SourceFullName = shape.ForeignData.ObjectSourceFullName ?? string.Empty,
                            DataSize = shape.ForeignData.Value.Length
                        };

                        oleInfos.Add(info);
                    }
                }
            }

            // Serialize the collected OLE information to JSON.
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(oleInfos, jsonOptions);

            // Write JSON to the output file.
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"OLE object information serialized to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple DTO for JSON serialization of OLE object details.
    private class OleInfo
    {
        public int PageIndex { get; set; }
        public long ShapeId { get; set; }
        public string ObjectType { get; set; }
        public string SourceFullName { get; set; }
        public int DataSize { get; set; }
    }
}