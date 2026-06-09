using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the source Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Find the existing "Design" layer
                Layer designLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Design")
                    {
                        designLayer = layer;
                        break;
                    }
                }

                if (designLayer == null)
                {
                    throw new Exception("Design layer not found in the diagram.");
                }

                // Create a new layer named "Prototype"
                Layer prototypeLayer = new Layer();
                prototypeLayer.Name.Value = "Prototype";
                prototypeLayer.Visible.Value = BOOL.True;
                prototypeLayer.IsColorChecked = BOOL.True; // required BOOL assignment
                page.PageSheet.Layers.Add(prototypeLayer);

                // After adding, the layer gets an index (IX). Use it for membership.
                int prototypeLayerIndex = prototypeLayer.IX;

                // Iterate all shapes on the page and copy membership from Design to Prototype
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get current layer membership string (e.g., "0;2")
                    string memberValue = shape.LayerMem.LayerMember.Value ?? string.Empty;
                    string[] memberIndices = memberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    // Check if shape belongs to the Design layer
                    bool isInDesign = false;
                    foreach (string idxStr in memberIndices)
                    {
                        if (int.TryParse(idxStr, out int idx) && idx == designLayer.IX)
                        {
                            isInDesign = true;
                            break;
                        }
                    }

                    if (isInDesign)
                    {
                        // Add Prototype layer index if not already present
                        bool alreadyInPrototype = false;
                        foreach (string idxStr in memberIndices)
                        {
                            if (int.TryParse(idxStr, out int idx) && idx == prototypeLayerIndex)
                            {
                                alreadyInPrototype = true;
                                break;
                            }
                        }

                        if (!alreadyInPrototype)
                        {
                            // Build new membership string
                            string newMemberValue = memberValue;
                            if (string.IsNullOrEmpty(newMemberValue))
                            {
                                newMemberValue = prototypeLayerIndex.ToString();
                            }
                            else
                            {
                                newMemberValue = newMemberValue + ";" + prototypeLayerIndex;
                            }

                            shape.LayerMem.LayerMember.Value = newMemberValue;
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }