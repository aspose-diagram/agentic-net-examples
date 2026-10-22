using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Build a lookup of layer index -> layer name for quick reference.
            Dictionary<int, string> layerIndexToName = new Dictionary<int, string>();
            foreach (Page page in diagram.Pages)
            {
                // Each page can have its own layer collection.
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Layer.IX is the integer index of the layer.
                    // Layer.Name.Value holds the layer's display name.
                    int ix = layer.IX;
                    string name = layer.Name?.Value ?? $"Layer_{ix}";
                    if (!layerIndexToName.ContainsKey(ix))
                    {
                        layerIndexToName.Add(ix, name);
                    }
                }
            }

            // Prepare the CSV output file.
            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                // Write CSV header.
                writer.WriteLine("PageIndex,ShapeID,ShapeName,MasterName,LayerNames");

                // Iterate through pages and shapes.
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve basic shape information.
                        long shapeId = shape.ID;
                        string shapeName = shape.NameU ?? string.Empty;
                        string masterName = shape.Master != null ? shape.Master.Name ?? string.Empty : string.Empty;

                        // Retrieve layer membership string (e.g., "1,3").
                        string layerMemberValue = shape.LayerMem?.LayerMember?.Value ?? string.Empty;
                        string layerNames = string.Empty;

                        if (!string.IsNullOrWhiteSpace(layerMemberValue))
                        {
                            // Split the indices, map to names, and join with semicolon.
                            string[] parts = layerMemberValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            List<string> names = new List<string>();
                            foreach (string part in parts)
                            {
                                if (int.TryParse(part.Trim(), out int ix) && layerIndexToName.TryGetValue(ix, out string lname))
                                {
                                    names.Add(lname);
                                }
                                else
                                {
                                    // Fallback to the raw index if name not found.
                                    names.Add($"Layer_{part.Trim()}");
                                }
                            }
                            layerNames = string.Join(";", names);
                        }

                        // Write the CSV line for the current shape.
                        // Escape commas in text fields by surrounding with double quotes.
                        writer.WriteLine($"{pageIndex},{shapeId},\"{shapeName}\",\"{masterName}\",\"{layerNames}\"");
                    }

                    pageIndex++;
                }
            }

            Console.WriteLine($"CSV mapping file created at \"{outputCsvPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}