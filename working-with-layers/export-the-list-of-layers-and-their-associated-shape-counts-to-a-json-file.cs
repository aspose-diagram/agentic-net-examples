using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputJsonFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // List to hold layer information across all pages
            List<LayerInfo> layerInfos = new List<LayerInfo>();

            // Iterate through each page in the diagram
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each layer on the current page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    int shapeCount = 0;
                    string layerIxString = layer.IX.ToString();

                    // Count shapes that belong to this layer
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the layer membership string (semicolon-separated)
                        string memberValue = shape.LayerMem?.LayerMember?.Value ?? string.Empty;
                        if (string.IsNullOrEmpty(memberValue))
                            continue;

                        // Split and compare each index
                        string[] members = memberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string member in members)
                        {
                            if (member.Trim() == layerIxString)
                            {
                                shapeCount++;
                                break; // shape counted once per layer
                            }
                        }
                    }

                    // Add the collected info to the list
                    layerInfos.Add(new LayerInfo
                    {
                        PageIndex = pageIndex,
                        LayerName = layer.Name?.Value ?? string.Empty,
                        ShapeCount = shapeCount
                    });
                }

                pageIndex++;
            }

            // Serialize the result to JSON with indentation
            string json = JsonSerializer.Serialize(layerInfos, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to the output file
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Layer information exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Helper class to represent layer information
    private class LayerInfo
    {
        public int PageIndex { get; set; }
        public string LayerName { get; set; }
        public int ShapeCount { get; set; }
    }
}