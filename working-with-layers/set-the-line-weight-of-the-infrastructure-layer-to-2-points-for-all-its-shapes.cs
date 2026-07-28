using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from the file system
                Diagram diagram = new Diagram(inputPath);

                // Locate the 'Infrastructure' layer and obtain its index (IX)
                int infraLayerIndex = -1;
                // Layers are stored in the first page's PageSheet collection
                foreach (Layer layer in diagram.Pages[0].PageSheet.Layers)
                {
                    if (layer.Name.Value == "Infrastructure")
                    {
                        infraLayerIndex = layer.IX;
                        break;
                    }
                }

                if (infraLayerIndex == -1)
                {
                    throw new Exception("Layer named 'Infrastructure' was not found in the diagram.");
                }

                // Convert 2 points to inches (Visio stores line weight in inches)
                double lineWeightInInches = 2.0 / 72.0;

                // Iterate through all pages and shapes, applying the line weight to shapes on the target layer
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the layer membership string (e.g., "0;2;5")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        if (string.IsNullOrEmpty(layerMember))
                            continue; // Shape is not assigned to any layer

                        // Split the semicolon‑separated list and check for the target layer index
                        string[] members = layerMember.Split(';');
                        foreach (string member in members)
                        {
                            if (int.TryParse(member, out int idx) && idx == infraLayerIndex)
                            {
                                // Set the line weight for the shape (in inches)
                                shape.Line.LineWeight.Value = lineWeightInInches;
                                break; // No need to check other members for this shape
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }