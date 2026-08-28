using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path (replace with actual path)
                string inputPath = "input.vsdx";
                // Output CSV file path
                string outputCsv = "AnalysisShapes.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Find the index of the layer named "Analysis" (search in the first page's layer collection)
                int analysisLayerIndex = -1;
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];
                    foreach (Layer layer in firstPage.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Analysis")
                        {
                            analysisLayerIndex = layer.IX;
                            break;
                        }
                    }
                }

                if (analysisLayerIndex == -1)
                {
                    Console.WriteLine("Layer 'Analysis' not found in the diagram.");
                    return;
                }

                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(outputCsv, false))
                {
                    // Write CSV header
                    writer.WriteLine("ID,Name,Text");

                    // Iterate all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve layer membership string (e.g., "0;2")
                            string layerMember = shape.LayerMem.LayerMember.Value;
                            if (string.IsNullOrEmpty(layerMember))
                                continue;

                            // Check if the shape belongs to the Analysis layer
                            string[] members = layerMember.Split(';');
                            bool belongsToAnalysis = false;
                            foreach (string member in members)
                            {
                                if (int.TryParse(member, out int idx) && idx == analysisLayerIndex)
                                {
                                    belongsToAnalysis = true;
                                    break;
                                }
                            }

                            if (!belongsToAnalysis)
                                continue;

                            // Extract shape information
                            string shapeId = shape.ID.ToString();
                            string shapeName = shape.Name ?? "";
                            string shapeText = shape.Text?.Value?.Text ?? "";

                            // Escape double quotes in text fields
                            shapeName = shapeName.Replace("\"", "\"\"");
                            shapeText = shapeText.Replace("\"", "\"\"");

                            // Write CSV line
                            writer.WriteLine($"{shapeId},\"{shapeName}\",\"{shapeText}\"");
                        }
                    }
                }

                Console.WriteLine($"Export completed. CSV saved to: {outputCsv}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }