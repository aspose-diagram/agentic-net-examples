using System;
using System.IO;
using Aspose.Diagram;

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
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Find the index of the layer named "Analysis"
                    int analysisLayerIndex = -1;
                    foreach (Page page in diagram.Pages)
                    {
                        // Layers are stored in the page's sheet
                        foreach (Layer layer in page.PageSheet.Layers)
                        {
                            if (layer.Name.Value == "Analysis")
                            {
                                analysisLayerIndex = layer.IX;
                                break;
                            }
                        }
                        if (analysisLayerIndex != -1)
                            break;
                    }

                    if (analysisLayerIndex == -1)
                    {
                        Console.WriteLine("Layer 'Analysis' not found in the document.");
                        return;
                    }

                    // Prepare CSV writer
                    using (StreamWriter writer = new StreamWriter(outputCsv, false, System.Text.Encoding.UTF8))
                    {
                        // Write CSV header
                        writer.WriteLine("PageName,ShapeID,ShapeName,ShapeText");

                        // Iterate all pages and shapes
                        foreach (Page page in diagram.Pages)
                        {
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
                                bool belongsToAnalysis = false;
                                foreach (string idxStr in memberIndexes)
                                {
                                    if (int.TryParse(idxStr, out int idx) && idx == analysisLayerIndex)
                                    {
                                        belongsToAnalysis = true;
                                        break;
                                    }
                                }

                                if (!belongsToAnalysis)
                                    continue;

                                // Retrieve shape text (plain text)
                                string shapeText = shape.Text.Value.ToString();
                                // Escape double quotes in CSV fields
                                shapeText = shapeText.Replace("\"", "\"\"");

                                // Write CSV line
                                writer.WriteLine($"{page.Name},{shape.ID},{shape.Name},\"{shapeText}\"");
                            }
                        }
                    }

                    Console.WriteLine($"CSV report generated at: {outputCsv}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }