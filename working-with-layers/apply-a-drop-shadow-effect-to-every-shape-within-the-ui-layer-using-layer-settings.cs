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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the document
            foreach (Page page in diagram.Pages)
            {
                // Locate the layer named "UI"
                Layer uiLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "UI")
                    {
                        uiLayer = layer;
                        break;
                    }
                }

                // If the UI layer does not exist on this page, skip it
                if (uiLayer == null)
                    continue;

                // The layer index is stored in the IX property
                string uiLayerIndex = uiLayer.IX.ToString();

                // Apply drop shadow to every shape that belongs to the UI layer
                foreach (Shape shape in page.Shapes)
                {
                    // Ignore shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the layer membership string (semicolon‑separated indexes)
                    string member = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(member))
                        continue;

                    // Determine if the shape is assigned to the UI layer
                    bool belongsToUi = false;
                    foreach (string idx in member.Split(';'))
                    {
                        if (idx == uiLayerIndex)
                        {
                            belongsToUi = true;
                            break;
                        }
                    }

                    if (!belongsToUi)
                        continue;

                    // Configure a simple drop shadow for the shape
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // enable shadow
                    shape.Fill.ShdwForegnd.Value = "#000000";                    // shadow color (black)
                    shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30 % transparency
                    shape.Fill.ShapeShdwOffsetX.Value = 0.1;                     // horizontal offset (inches)
                    shape.Fill.ShapeShdwOffsetY.Value = 0.1;                     // vertical offset (inches)
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
