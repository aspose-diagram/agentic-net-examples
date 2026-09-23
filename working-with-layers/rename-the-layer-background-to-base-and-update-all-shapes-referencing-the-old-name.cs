using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the document
            foreach (Page page in diagram.Pages)
            {
                // Locate the layer named "Background"
                Layer targetLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Background")
                    {
                        targetLayer = layer;
                        break;
                    }
                }

                // If the layer exists, rename it to "Base"
                if (targetLayer != null)
                {
                    // Store the original layer index (IX) before renaming
                    int layerIndex = targetLayer.IX;

                    // Rename the layer
                    targetLayer.Name.Value = "Base";

                    // Update shapes that reference this layer
                    foreach (Shape shape in page.Shapes)
                    {
                        // The LayerMember cell contains a semicolon‑separated list of layer indexes
                        string member = shape.LayerMem.LayerMember.Value;
                        if (!string.IsNullOrEmpty(member))
                        {
                            // Check if the shape is assigned to the renamed layer
                            string[] indexes = member.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string idx in indexes)
                            {
                                if (int.TryParse(idx, out int i) && i == layerIndex)
                                {
                                    // Shape is already linked by index; no further action needed.
                                    // If you need to log the update, you can output the shape ID.
                                    Console.WriteLine($"Shape ID {shape.ID} remains on renamed layer \"Base\".");
                                    break;
                                }
                            }
                        }
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
