using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Name of the layer whose shapes should be protected
                string targetLayerName = "MyLayer";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Find the target layer index on the first page (layers are stored per page sheet)
                int? targetLayerIndex = null;
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];
                    foreach (Layer layer in firstPage.PageSheet.Layers)
                    {
                        if (layer.Name.Value == targetLayerName)
                        {
                            targetLayerIndex = layer.IX;
                            break;
                        }
                    }
                }

                if (targetLayerIndex == null)
                {
                    Console.WriteLine($"Layer \"{targetLayerName}\" not found. No protection applied.");
                    return;
                }

                // Apply protection to all shapes that belong to the target layer on every page
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has layer membership information
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                            continue;

                        string memberValue = shape.LayerMem.LayerMember.Value ?? string.Empty;
                        // LayerMember stores semicolon‑separated layer indexes (e.g., "0;2")
                        string[] memberIndexes = memberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        // Check if the shape is assigned to the target layer
                        bool isInTargetLayer = false;
                        foreach (string idxStr in memberIndexes)
                        {
                            if (int.TryParse(idxStr, out int idx) && idx == targetLayerIndex.Value)
                            {
                                isInTargetLayer = true;
                                break;
                            }
                        }

                        if (!isInTargetLayer)
                            continue;

                        // Apply desired protection flags
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        shape.Protection.LockMoveY.Value = BOOL.True;
                        shape.Protection.LockWidth.Value = BOOL.True;
                        shape.Protection.LockHeight.Value = BOOL.True;
                        shape.Protection.LockRotate.Value = BOOL.True;
                        shape.Protection.LockVtxEdit.Value = BOOL.True;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Shape protection applied and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }