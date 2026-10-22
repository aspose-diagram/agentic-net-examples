using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Model classes for JSON serialization
    private class FieldInfo
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public string Calendar { get; set; }
    }

    private class ShapeInfo
    {
        public long Id { get; set; }
        public string NameU { get; set; }
        public List<FieldInfo> Fields { get; set; }
    }

    private class PageInfo
    {
        public int Index { get; set; }
        public List<ShapeInfo> Shapes { get; set; }
    }

    static void Main(string[] args)
    {
        // Validate arguments
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

            // Prepare collection to hold extracted information
            List<PageInfo> pagesData = new List<PageInfo>();
            int pageIndex = 0;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                PageInfo pageInfo = new PageInfo
                {
                    Index = pageIndex,
                    Shapes = new List<ShapeInfo>()
                };

                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only non-deleted shapes
                    if (shape.Del == BOOL.False)
                    {
                        ShapeInfo shapeInfo = new ShapeInfo
                        {
                            Id = shape.ID,
                            NameU = shape.NameU,
                            Fields = new List<FieldInfo>()
                        };

                        // Iterate through all fields of the shape
                        foreach (Field field in shape.Fields)
                        {
                            FieldInfo fieldInfo = new FieldInfo
                            {
                                // Extract the field's textual value
                                Value = field.Value?.Val,
                                // Extract the field type as string (enum)
                                Type = field.Type?.Value.ToString(),
                                // Extract calendar information if present
                                Calendar = field.Calendar?.Value.ToString()
                            };

                            shapeInfo.Fields.Add(fieldInfo);
                        }

                        pageInfo.Shapes.Add(shapeInfo);
                    }
                }

                pagesData.Add(pageInfo);
                pageIndex++;
            }

            // Serialize the collected data to JSON with indentation
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonOutput = JsonSerializer.Serialize(pagesData, jsonOptions);

            // Write JSON to the specified output file
            File.WriteAllText(outputPath, jsonOutput);
            Console.WriteLine($"Field information exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations or I/O
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}