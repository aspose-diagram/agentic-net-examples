using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);
            Page page = diagram.Pages[0];

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
                throw new Exception("Layer 'Draft' not found in the diagram.");
            }

            Layer finalLayer = new Layer();
            finalLayer.Name.Value = "Final";
            finalLayer.Visible.Value = draftLayer.Visible.Value;
            finalLayer.IsColorChecked = draftLayer.IsColorChecked;
            page.PageSheet.Layers.Add(finalLayer);

            int draftIndex = draftLayer.IX;
            int finalIndex = finalLayer.IX;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                {
                    string members = shape.LayerMem.LayerMember.Value;
                    if (!string.IsNullOrEmpty(members) &&
                        (members == draftIndex.ToString() || members.Split(';').Contains(draftIndex.ToString())))
                    {
                        if (!members.Split(';').Contains(finalIndex.ToString()))
                        {
                            string newMembers = string.IsNullOrEmpty(members)
                                ? finalIndex.ToString()
                                : members + ";" + finalIndex.ToString();
                            shape.LayerMem.LayerMember.Value = newMembers;
                        }
                    }
                }
            }

            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}