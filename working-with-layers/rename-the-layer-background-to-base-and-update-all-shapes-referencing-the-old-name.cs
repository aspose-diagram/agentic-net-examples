using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Store the index of the layer named "Background"
                int oldLayerIndex = -1;

                // Rename the layer "Background" to "Base" on every page
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Background")
                        {
                            Console.WriteLine($"Renaming layer on page '{page.Name}' from 'Background' to 'Base'.");
                            layer.Name.Value = "Base";
                            oldLayerIndex = layer.IX; // capture the layer's index for later use
                        }
                    }
                }

                if (oldLayerIndex == -1)
                {
                    Console.WriteLine("Layer named 'Background' was not found in the document.");
                }
                else
                {
                    // Update shapes that reference the old layer index (membership string)
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape has a LayerMem object
                            if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                            {
                                string memberValue = shape.LayerMem.LayerMember.Value;
                                if (!string.IsNullOrEmpty(memberValue))
                                {
                                    // Split the semicolon‑separated list of layer indexes
                                    string[] indices = memberValue.Split(';');
                                    bool needsUpdate = false;

                                    for (int i = 0; i < indices.Length; i++)
                                    {
                                        if (int.TryParse(indices[i], out int idx) && idx == oldLayerIndex)
                                        {
                                            // The shape already references the renamed layer by index.
                                            // No change to the index is required because the index stays the same.
                                            // This block is kept for completeness and future logic.
                                            needsUpdate = true;
                                        }
                                    }

                                    if (needsUpdate)
                                    {
                                        // No modification needed; the index remains valid.
                                        // If additional processing were required, it would be placed here.
                                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' references the renamed layer.");
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }