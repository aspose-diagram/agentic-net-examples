using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Find the source layer named "Draft"
                Layer draftLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Draft")
                    {
                        draftLayer = layer;
                        break;
                    }
                }

                if (draftLayer == null)
                {
                    throw new Exception("Layer named 'Draft' was not found.");
                }

                // Create a new layer and copy relevant properties from the Draft layer
                Layer finalLayer = new Layer();
                finalLayer.Name.Value = "Final";
                finalLayer.Visible.Value = draftLayer.Visible.Value;
                finalLayer.Print.Value = draftLayer.Print.Value;
                // Add the new layer to the page's layer collection
                page.PageSheet.Layers.Add(finalLayer);

                // Retrieve the indexes of the original and cloned layers
                int draftIndex = draftLayer.IX;
                int finalIndex = finalLayer.IX;

                // Assign shapes that belong to the Draft layer to also belong to the Final layer
                foreach (Shape shape in page.Shapes)
                {
                    string memberValue = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(memberValue))
                        continue;

                    // Split the semicolon‑separated list of layer indexes
                    string[] indexes = memberValue.Split(';');
                    // Check if the shape is on the Draft layer
                    if (Array.Exists(indexes, i => i == draftIndex.ToString()))
                    {
                        // Ensure the shape also references the new Final layer
                        List<string> updated = new List<string>(indexes);
                        if (!updated.Contains(finalIndex.ToString()))
                        {
                            updated.Add(finalIndex.ToString());
                            shape.LayerMem.LayerMember.Value = string.Join(";", updated);
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }