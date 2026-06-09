using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths – adjust as needed.
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the document.
                foreach (Page page in diagram.Pages)
                {
                    // Locate the layer named "Background".
                    Layer targetLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Background")
                        {
                            targetLayer = layer;
                            break;
                        }
                    }

                    // If the layer exists, rename it to "Base".
                    if (targetLayer != null)
                    {
                        // Preserve the layer index (IX) for later reference.
                        int layerIndex = targetLayer.IX;
                        targetLayer.Name.Value = "Base";

                        // Update all shapes on this page that belong to the renamed layer.
                        foreach (Shape shape in page.Shapes)
                        {
                            // The LayerMember cell holds a semicolon‑separated list of layer indexes.
                            string memberValue = shape.LayerMem.LayerMember.Value;

                            // If the shape already references the layer index, nothing to change.
                            // Otherwise, add the layer index to the membership list.
                            if (!string.IsNullOrEmpty(memberValue))
                            {
                                string[] indices = memberValue.Split(';');
                                bool alreadyMember = false;
                                foreach (string idx in indices)
                                {
                                    if (int.TryParse(idx, out int existingIdx) && existingIdx == layerIndex)
                                    {
                                        alreadyMember = true;
                                        break;
                                    }
                                }

                                if (!alreadyMember)
                                {
                                    shape.LayerMem.LayerMember.Value = memberValue + ";" + layerIndex;
                                }
                            }
                            else
                            {
                                // Shape had no layer membership; assign the renamed layer.
                                shape.LayerMem.LayerMember.Value = layerIndex.ToString();
                            }
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }