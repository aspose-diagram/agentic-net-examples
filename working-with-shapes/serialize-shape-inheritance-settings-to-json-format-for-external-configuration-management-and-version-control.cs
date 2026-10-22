using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Model class to hold inheritance information for a shape
    private class ShapeInheritanceInfo
    {
        public int PageIndex { get; set; }
        public long ShapeId { get; set; }
        public string NameU { get; set; }
        public string MasterName { get; set; }
        public bool InheritFill { get; set; }
        public bool InheritLine { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output JSON file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
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
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                var inheritanceList = new List<ShapeInheritanceInfo>();

                // Iterate through all pages
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];

                    // Iterate through all shapes on the current page
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        Shape shape = page.Shapes[shapeIdx];

                        // Determine fill inheritance by comparing current fill foregnd with inherited value
                        bool inheritFill = false;
                        if (shape.InheritFill != null && shape.Fill != null && shape.InheritFill.FillForegnd != null && shape.Fill.FillForegnd != null)
                        {
                            inheritFill = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                        }

                        // Determine line inheritance by comparing current line color with inherited value
                        bool inheritLine = false;
                        if (shape.InheritLine != null && shape.Line != null && shape.InheritLine.LineColor != null && shape.Line.LineColor != null)
                        {
                            inheritLine = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                        }

                        // Collect information into the model
                        var info = new ShapeInheritanceInfo
                        {
                            PageIndex = pageIdx,
                            ShapeId = shape.ID,
                            NameU = shape.NameU,
                            MasterName = shape.Master != null ? shape.Master.Name : string.Empty,
                            InheritFill = inheritFill,
                            InheritLine = inheritLine
                        };

                        inheritanceList.Add(info);
                    }
                }

                // Serialize the list to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(inheritanceList, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Inheritance settings have been serialized to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}