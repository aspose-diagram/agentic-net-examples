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

                // Find the index of the layer named "ReadOnly"
                string readOnlyLayerIndex = null;
                // Layers are stored in the PageSheet of each page; they are usually identical across pages.
                // Use the first page to locate the layer.
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];
                    foreach (Layer layer in firstPage.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "ReadOnly")
                        {
                            readOnlyLayerIndex = layer.IX.ToString();
                            break;
                        }
                    }
                }

                if (readOnlyLayerIndex == null)
                {
                    Console.WriteLine("Layer \"ReadOnly\" not found. No protection applied.");
                    return;
                }

                // Apply protection to all shapes that belong to the "ReadOnly" layer
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Layer membership is a semicolon‑separated list of layer indexes
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Check if the shape is assigned to the ReadOnly layer
                        string[] assignedLayers = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        bool belongsToReadOnly = false;
                        foreach (string idx in assignedLayers)
                        {
                            if (idx == readOnlyLayerIndex)
                            {
                                belongsToReadOnly = true;
                                break;
                            }
                        }

                        if (!belongsToReadOnly)
                            continue;

                        // Apply full protection
                        shape.Protection.LockAspect.Value = BOOL.True;
                        shape.Protection.LockBegin.Value = BOOL.True;
                        shape.Protection.LockCalcWH.Value = BOOL.True;
                        shape.Protection.LockCrop.Value = BOOL.True;
                        shape.Protection.LockCustProp.Value = BOOL.True;
                        shape.Protection.LockDelete.Value = BOOL.True;
                        shape.Protection.LockEnd.Value = BOOL.True;
                        shape.Protection.LockFormat.Value = BOOL.True;
                        shape.Protection.LockFromGroupFormat.Value = BOOL.True;
                        shape.Protection.LockGroup.Value = BOOL.True;
                        shape.Protection.LockHeight.Value = BOOL.True;
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        shape.Protection.LockMoveY.Value = BOOL.True;
                        shape.Protection.LockRotate.Value = BOOL.True;
                        shape.Protection.LockSelect.Value = BOOL.True;
                        shape.Protection.LockTextEdit.Value = BOOL.True;
                        shape.Protection.LockThemeColors.Value = BOOL.True;
                        shape.Protection.LockThemeEffects.Value = BOOL.True;
                        shape.Protection.LockVtxEdit.Value = BOOL.True;
                        shape.Protection.LockWidth.Value = BOOL.True;
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx"; // TODO: replace with desired output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Protection applied to shapes on the \"ReadOnly\" layer and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }