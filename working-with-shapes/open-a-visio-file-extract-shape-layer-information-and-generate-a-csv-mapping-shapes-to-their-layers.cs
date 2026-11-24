using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];

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

            // Prepare a mapping from layer index to layer name for quick lookup
            Dictionary<int, string> layerIndexToName = new Dictionary<int, string>();

            // Iterate through all pages to collect layers (layers are page-specific)
            foreach (Page page in diagram.Pages)
            {
                // Ensure the page has a Layers collection
                if (page.PageSheet != null && page.PageSheet.Layers != null)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // layer.IX is the index (int) and layer.Name.Value holds the name
                        int ix = layer.IX;
                        string name = layer.Name?.Value ?? $"Layer_{ix}";
                        // Store or overwrite; layer indices are unique per page
                        layerIndexToName[ix] = name;
                    }
                }
            }

            // Open CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                // Write CSV header
                writer.WriteLine("ShapeId,ShapeName,Layers");

                // Iterate through pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape ID
                        long shapeId = shape.ID;

                        // Retrieve shape name (fallback to empty string if null)
                        string shapeName = shape.NameU ?? string.Empty;

                        // Retrieve layer membership string (semicolon-separated indices)
                        string layerMemberValue = shape.LayerMem?.LayerMember?.Value ?? string.Empty;

                        // Prepare list of layer names
                        List<string> layerNames = new List<string>();

                        if (!string.IsNullOrWhiteSpace(layerMemberValue))
                        {
                            // Split by semicolon as per API specification
                            string[] parts = layerMemberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string part in parts)
                            {
                                // Trim whitespace and parse index
                                if (int.TryParse(part.Trim(), out int ix))
                                {
                                    if (layerIndexToName.TryGetValue(ix, out string layerName))
                                    {
                                        layerNames.Add(layerName);
                                    }
                                    else
                                    {
                                        // If layer index not found, keep the raw index
                                        layerNames.Add($"Index_{ix}");
                                    }
                                }
                            }
                        }

                        // Join layer names with semicolon for CSV column
                        string layersCsv = string.Join(";", layerNames);

                        // Escape commas in shape name or layers if needed by surrounding with quotes
                        if (shapeName.Contains(",") || shapeName.Contains("\""))
                        {
                            shapeName = $"\"{shapeName.Replace("\"", "\"\"")}\"";
                        }
                        if (layersCsv.Contains(",") || layersCsv.Contains("\""))
                        {
                            layersCsv = $"\"{layersCsv.Replace("\"", "\"\"")}\"";
                        }

                        // Write CSV line
                        writer.WriteLine($"{shapeId},{shapeName},{layersCsv}");
                    }
                }
            }

            Console.WriteLine($"CSV file generated successfully at: {outputCsvPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}