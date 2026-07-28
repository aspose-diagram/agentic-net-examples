using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (replace with actual paths as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page; adjust if multiple pages are needed
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
                    Console.WriteLine("Design layer not found. No shapes will be copied.");
                    return;
                }

                // Create a new layer named "Prototype"
                Layer prototypeLayer = new Layer();
                prototypeLayer.Name.Value = "Prototype";
                prototypeLayer.Visible.Value = BOOL.True;
                // Add the new layer to the page's layer collection
                page.PageSheet.Layers.Add(prototypeLayer);

                // Get the string representations of the layer indexes
                string designIndex = designLayer.IX.ToString();
                string prototypeIndex = prototypeLayer.IX.ToString();

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the current layer membership string (e.g., "0;2")
                    string currentMembership = shape.LayerMem.LayerMember.Value;

                    // Check if the shape belongs to the Design layer
                    var memberIndexes = currentMembership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (memberIndexes.Contains(designIndex))
                    {
                        // If the shape is not already on the Prototype layer, add it
                        if (!memberIndexes.Contains(prototypeIndex))
                        {
                            // Append the new layer index
                            string newMembership = string.IsNullOrEmpty(currentMembership)
                                ? prototypeIndex
                                : currentMembership + ";" + prototypeIndex;

                            shape.LayerMem.LayerMember.Value = newMembership;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Shapes copied to the Prototype layer and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }