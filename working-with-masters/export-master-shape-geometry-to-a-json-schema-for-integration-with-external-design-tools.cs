using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Simple DTO for JSON serialization of a shape's geometry
    private class ShapeInfo
    {
        public long Id { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    // DTO for a master and its constituent shapes
    private class MasterInfo
    {
        public string Name { get; set; }
        public List<ShapeInfo> Shapes { get; set; }
    }

    // Root object for the exported JSON schema
    private class DiagramSchema
    {
        public List<MasterInfo> Masters { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output JSON file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputJsonFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare a collection to hold master geometry information
            List<MasterInfo> mastersInfo = new List<MasterInfo>();

            // Iterate over all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Ensure the master has a valid name
                string masterName = master.Name ?? string.Empty;

                MasterInfo masterInfo = new MasterInfo
                {
                    Name = masterName,
                    Shapes = new List<ShapeInfo>()
                };

                // Iterate over shapes that belong to this master
                for (int i = 0; i < master.Shapes.Count; i++)
                {
                    Shape shape = master.Shapes[i];

                    // Extract basic geometry data from the shape's XForm section
                    ShapeInfo shapeInfo = new ShapeInfo
                    {
                        Id = shape.ID,
                        PinX = shape.XForm.PinX.Value,
                        PinY = shape.XForm.PinY.Value,
                        Width = shape.XForm.Width.Value,
                        Height = shape.XForm.Height.Value
                    };

                    masterInfo.Shapes.Add(shapeInfo);
                }

                mastersInfo.Add(masterInfo);
            }

            // Build the final schema object
            DiagramSchema schema = new DiagramSchema
            {
                Masters = mastersInfo
            };

            // Serialize to JSON with indentation for readability
            string json = JsonSerializer.Serialize(schema, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the specified output file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Master geometry exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}