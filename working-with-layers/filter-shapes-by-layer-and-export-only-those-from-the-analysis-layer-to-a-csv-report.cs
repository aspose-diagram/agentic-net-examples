using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

namespace DiagramLayerExport
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output CSV file path
                string outputCsv = "AnalysisLayerShapes.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare list to hold CSV rows
                List<string> csvLines = new List<string>();
                // Header row
                csvLines.Add("ShapeID,ShapeName,ShapeText");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Find the index (IX) of the layer named "Analysis"
                    int analysisLayerIndex = -1;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Analysis")
                        {
                            analysisLayerIndex = layer.IX;
                            break;
                        }
                    }

                    // If the layer does not exist on this page, skip it
                    if (analysisLayerIndex == -1)
                        continue;

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the layer membership string (e.g., "0;2;5")
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Check if the shape belongs to the Analysis layer
                        string[] memberIndexes = layerMember.Split(';');
                        bool isInAnalysisLayer = false;
                        foreach (string idxStr in memberIndexes)
                        {
                            if (int.TryParse(idxStr, out int idx) && idx == analysisLayerIndex)
                            {
                                isInAnalysisLayer = true;
                                break;
                            }
                        }

                        if (!isInAnalysisLayer)
                            continue;

                        // Retrieve shape information
                        long shapeId = shape.ID;
                        string shapeName = shape.Name ?? "";
                        // Get plain text of the shape
                        string shapeText = shape.Text?.Value?.Text ?? "";

                        // Escape double quotes in CSV fields
                        shapeName = shapeName.Replace("\"", "\"\"");
                        shapeText = shapeText.Replace("\"", "\"\"");

                        // Build CSV line
                        string csvLine = $"{shapeId},\"{shapeName}\",\"{shapeText}\"";
                        csvLines.Add(csvLine);
                    }
                }

                // Write all lines to the CSV file
                File.WriteAllLines(outputCsv, csvLines);

                Console.WriteLine($"Export completed. {csvLines.Count - 1} shape(s) written to '{outputCsv}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}
