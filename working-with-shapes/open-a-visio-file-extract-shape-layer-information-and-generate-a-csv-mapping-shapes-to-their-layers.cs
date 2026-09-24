using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output CSV file path
            string outputCsv = "shape_layers.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare CSV lines with header
            List<string> csvLines = new List<string>();
            csvLines.Add("ShapeID,ShapeName,LayerNames");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Build a map of layer index to layer name for the current page
                Dictionary<int, string> layerMap = new Dictionary<int, string>();
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // layer.IX is the zero‑based index of the layer
                    layerMap[layer.IX] = layer.Name.Value;
                }

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    long shapeId = shape.ID;
                    string shapeName = shape.NameU ?? string.Empty;

                    // Retrieve the layer membership string (semicolon‑separated indexes)
                    string layerMember = shape.LayerMem.LayerMember.Value;
                    string layerNames = string.Empty;

                    if (!string.IsNullOrEmpty(layerMember))
                    {
                        string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> names = new List<string>();
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part, out int idx) && layerMap.TryGetValue(idx, out string lname))
                            {
                                names.Add(lname);
                            }
                        }
                        layerNames = string.Join(";", names);
                    }

                    // Escape commas in CSV fields if necessary
                    string escapedName = shapeName.Contains(",") ? $"\"{shapeName}\"" : shapeName;
                    string escapedLayers = layerNames.Contains(",") ? $"\"{layerNames}\"" : layerNames;

                    csvLines.Add($"{shapeId},{escapedName},{escapedLayers}");
                }
            }

            // Write all lines to the CSV file
            File.WriteAllLines(outputCsv, csvLines);
            Console.WriteLine($"CSV mapping saved to: {outputCsv}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
