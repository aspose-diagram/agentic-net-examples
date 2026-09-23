using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output CSV file path
                string outputCsv = "AnalysisLayerShapes.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare a list to hold CSV rows
                List<string> csvLines = new List<string>();
                // Add CSV header
                csvLines.Add("ShapeID,ShapeName,ShapeText");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Find the layer named "Analysis" on the current page
                    Layer analysisLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Analysis")
                        {
                            analysisLayer = layer;
                            break;
                        }
                    }

                    // If the layer does not exist on this page, skip to next page
                    if (analysisLayer == null)
                        continue;

                    // Get the index of the analysis layer as a string (e.g., "3")
                    string layerIndexStr = analysisLayer.IX.ToString();

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (e.g., "0;2;3")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        // If the shape has no layer membership, skip it
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Split the membership string and check for the analysis layer index
                        string[] memberIndexes = layerMember.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        bool isInAnalysisLayer = false;
                        foreach (string idx in memberIndexes)
                        {
                            if (idx == layerIndexStr)
                            {
                                isInAnalysisLayer = true;
                                break;
                            }
                        }

                        if (!isInAnalysisLayer)
                            continue;

                        // Extract shape details
                        long shapeId = shape.ID;
                        string shapeName = shape.NameU ?? string.Empty;
                        string shapeText = shape.Text.Value.Text ?? string.Empty;

                        // Escape commas in text fields
                        shapeName = shapeName.Replace(",", " ");
                        shapeText = shapeText.Replace(",", " ");

                        // Build CSV line
                        string csvLine = $"{shapeId},{shapeName},{shapeText}";
                        csvLines.Add(csvLine);
                    }
                }

                // Write all collected lines to the CSV file
                try
                {
                    File.WriteAllLines(outputCsv, csvLines);
                    Console.WriteLine($"Export completed. CSV saved to: {outputCsv}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing CSV file: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }