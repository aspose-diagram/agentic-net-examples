using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assume the watermark layer is on the first page
            Page page = diagram.Pages[0];

            // Find the index of the layer named "Watermark"
            int watermarkLayerIndex = -1;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value.Equals("Watermark", StringComparison.OrdinalIgnoreCase))
                {
                    watermarkLayerIndex = layer.IX;
                    break;
                }
            }

            if (watermarkLayerIndex == -1)
            {
                throw new Exception("Layer named 'Watermark' was not found.");
            }

            // Apply 50% transparency to all shapes that belong to the Watermark layer
            foreach (Shape shape in page.Shapes)
            {
                string member = shape.LayerMem.LayerMember.Value;
                if (string.IsNullOrEmpty(member))
                    continue;

                string[] indices = member.Split(';');
                foreach (string idxStr in indices)
                {
                    if (int.TryParse(idxStr, out int idx) && idx == watermarkLayerIndex)
                    {
                        // Set fill foreground transparency
                        shape.Fill.FillForegndTrans.Value = 50;
                        // Set line color transparency
                        shape.Line.LineColorTrans.Value = 50;
                        break;
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
