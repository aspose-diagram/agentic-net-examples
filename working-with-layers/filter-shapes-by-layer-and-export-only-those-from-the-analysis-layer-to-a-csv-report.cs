using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the 'Analysis' layer and obtain its index (IX).
                int analysisLayerIndex = -1;
                foreach (Page page in diagram.Pages)
                {
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
                    Console.Error.WriteLine("Layer 'Analysis' not found in the diagram.");
                    return;
                }

                // Iterate all shapes and mark those NOT belonging to the 'Analysis' layer as deleted.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip already deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (semicolon‑separated indexes).
                        string layerMember = shape.LayerMem?.LayerMember?.Value ?? string.Empty;

                        // Determine if the shape belongs to the target layer.
                        bool belongsToAnalysis = false;
                        if (!string.IsNullOrEmpty(layerMember))
                        {
                            string[] parts = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string part in parts)
                            {
                                if (part == analysisLayerIndex.ToString())
                                {
                                    belongsToAnalysis = true;
                                    break;
                                }
                            }
                        }

                        // If the shape is not on the 'Analysis' layer, mark it as deleted.
                        if (!belongsToAnalysis)
                        {
                            shape.Del = BOOL.True;
                        }
                    }
                }

                // Save the filtered diagram as a CSV report.
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"CSV report generated at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}