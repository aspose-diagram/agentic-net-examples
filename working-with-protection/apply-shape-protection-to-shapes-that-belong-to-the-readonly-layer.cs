using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (replace with actual paths)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Find the index of the layer named "ReadOnly"
                    int readOnlyLayerIndex = -1;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "ReadOnly")
                        {
                            readOnlyLayerIndex = layer.IX;
                            break;
                        }
                    }

                    // If the "ReadOnly" layer does not exist on this page, skip to next page
                    if (readOnlyLayerIndex == -1)
                        continue;

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get the layer membership string (e.g., "0;2;5")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Check if the shape belongs to the "ReadOnly" layer
                        bool belongsToReadOnly = false;
                        string[] members = layerMember.Split(';');
                        foreach (string member in members)
                        {
                            if (member == readOnlyLayerIndex.ToString())
                            {
                                belongsToReadOnly = true;
                                break;
                            }
                        }

                        if (!belongsToReadOnly)
                            continue;

                        // Apply full protection to the shape
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
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }