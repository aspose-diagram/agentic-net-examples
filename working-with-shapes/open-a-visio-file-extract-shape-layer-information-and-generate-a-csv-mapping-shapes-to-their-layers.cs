using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output CSV file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputCsvPath>");
            return;
        }

        // Assign input and output paths.
        string inputPath = args[0];
        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        // Guard: ensure output directory exists (create if missing).
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        // Prepare a list to hold CSV rows.
        List<string> csvLines = new List<string>();
        // Add CSV header.
        csvLines.Add("ShapeId,ShapeName,LayerName");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Build a map of layer index -> layer name for the current page.
                Dictionary<int, string> layerIndexToName = new Dictionary<int, string>();
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // layer.IX is the zero‑based index; layer.Name.Value holds the name.
                    layerIndexToName[layer.IX] = layer.Name.Value;
                }

                // Iterate through each shape on the page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True) continue;

                    // Retrieve shape identifier and universal name.
                    long shapeId = shape.ID;
                    // NameU is a plain string, not a wrapper with .Value.
                    string shapeName = shape.NameU ?? string.Empty;

                    // Get the semicolon‑separated list of layer indexes the shape belongs to.
                    string layerMember = shape.LayerMem.LayerMember.Value ?? string.Empty;

                    // If the shape is not assigned to any layer, still output a row with empty layer.
                    if (string.IsNullOrWhiteSpace(layerMember))
                    {
                        csvLines.Add($"{shapeId},\"{shapeName}\",");
                        continue;
                    }

                    // Split the indexes and map each to a layer name.
                    string[] indexTokens = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string token in indexTokens)
                    {
                        if (int.TryParse(token, out int layerIdx) && layerIndexToName.TryGetValue(layerIdx, out string layerName))
                        {
                            // Escape commas in names by surrounding with double quotes.
                            csvLines.Add($"{shapeId},\"{shapeName}\",\"{layerName}\"");
                        }
                        else
                        {
                            // If the index cannot be parsed or mapped, output with empty layer name.
                            csvLines.Add($"{shapeId},\"{shapeName}\",");
                        }
                    }
                }
            }

            // Write all collected CSV lines to the output file.
            File.WriteAllLines(outputPath, csvLines);
            Console.WriteLine($"CSV mapping created at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}