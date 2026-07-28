using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Name of the layer whose shapes should be protected
                string targetLayerName = "MyLayer";

                // Locate the target layer on the first page (layers are shared across pages)
                Layer targetLayer = null;
                foreach (Layer layer in diagram.Pages[0].PageSheet.Layers)
                {
                    if (layer.Name.Value == targetLayerName)
                    {
                        targetLayer = layer;
                        break;
                    }
                }

                if (targetLayer == null)
                {
                    Console.WriteLine($"Layer \"{targetLayerName}\" not found.");
                    return;
                }

                // The layer index is stored as a zero‑based integer in the IX property
                string layerIndex = targetLayer.IX.ToString();

                // Iterate through all pages and all shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the semicolon‑separated list of layer indexes the shape belongs to
                        string membership = shape.LayerMem.LayerMember.Value;

                        if (string.IsNullOrEmpty(membership))
                            continue;

                        // Check if the shape is assigned to the target layer
                        string[] members = membership.Split(';');
                        foreach (string member in members)
                        {
                            if (member == layerIndex)
                            {
                                // Apply protection flags to the shape
                                shape.Protection.LockMoveX.Value = BOOL.True;
                                shape.Protection.LockMoveY.Value = BOOL.True;
                                shape.Protection.LockWidth.Value = BOOL.True;
                                shape.Protection.LockHeight.Value = BOOL.True;
                                shape.Protection.LockRotate.Value = BOOL.True;
                                shape.Protection.LockVtxEdit.Value = BOOL.True;
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Shape protection applied and diagram saved as output.vsdx.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }