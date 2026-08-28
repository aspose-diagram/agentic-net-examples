using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx"; // TODO: replace with actual file path
                Diagram diagram = new Diagram(inputPath);

                // Desired layer name
                const string targetLayerName = "Infrastructure";

                // Desired line weight in points (2 points = 2/72 inches)
                double lineWeightInInches = 2.0 / 72.0;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Find the index of the target layer on this page
                    int? targetLayerIndex = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == targetLayerName)
                        {
                            targetLayerIndex = layer.IX;
                            break;
                        }
                    }

                    // If the layer does not exist on this page, skip it
                    if (!targetLayerIndex.HasValue)
                        continue;

                    string targetIndexString = targetLayerIndex.Value.ToString();

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has layer membership information
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                            continue;

                        // The LayerMember.Value holds a semicolon‑separated list of layer indexes
                        string memberValue = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(memberValue))
                            continue;

                        // Check if the shape belongs to the target layer
                        string[] indexes = memberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        bool belongsToTarget = false;
                        foreach (string idx in indexes)
                        {
                            if (idx == targetIndexString)
                            {
                                belongsToTarget = true;
                                break;
                            }
                        }

                        if (!belongsToTarget)
                            continue;

                        // Set the line weight for the shape
                        if (shape.Line != null && shape.Line.LineWeight != null)
                        {
                            shape.Line.LineWeight.Value = lineWeightInInches;
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx"; // TODO: replace with desired output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }