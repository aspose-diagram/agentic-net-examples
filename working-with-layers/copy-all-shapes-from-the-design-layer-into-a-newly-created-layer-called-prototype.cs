using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Find the index of the "Design" layer on this page
                int designLayerIndex = -1;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Design")
                    {
                        designLayerIndex = layer.IX;
                        break;
                    }
                }

                // If the "Design" layer does not exist on this page, skip it
                if (designLayerIndex == -1)
                    continue;

                // Ensure the "Prototype" layer exists; create it if necessary
                Layer prototypeLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Prototype")
                    {
                        prototypeLayer = layer;
                        break;
                    }
                }

                if (prototypeLayer == null)
                {
                    prototypeLayer = new Layer();
                    prototypeLayer.Name.Value = "Prototype";
                    prototypeLayer.Visible.Value = BOOL.True;
                    prototypeLayer.IsColorChecked = BOOL.False;
                    page.PageSheet.Layers.Add(prototypeLayer);
                }

                int prototypeIndex = prototypeLayer.IX;

                // Add each shape that belongs to the "Design" layer to the "Prototype" layer
                foreach (Shape shape in page.Shapes)
                {
                    string layerMember = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(layerMember))
                        continue;

                    // Check if the shape is on the "Design" layer
                    List<string> members = layerMember.Split(';').ToList();
                    if (members.Contains(designLayerIndex.ToString()))
                    {
                        // Add the prototype layer index if it's not already present
                        if (!members.Contains(prototypeIndex.ToString()))
                        {
                            members.Add(prototypeIndex.ToString());
                            shape.LayerMem.LayerMember.Value = string.Join(";", members);
                        }
                    }
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
