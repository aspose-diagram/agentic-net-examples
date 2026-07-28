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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Find the index of the layer named "UI"
                int uiLayerIndex = -1;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "UI")
                    {
                        uiLayerIndex = layer.IX;
                        break;
                    }
                }

                // If the "UI" layer exists, apply shadow to its shapes
                if (uiLayerIndex != -1)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the layer membership string (e.g., "0;2;5")
                        string layerMember = shape.LayerMem.LayerMember.Value;

                        // Check if the shape belongs to the UI layer
                        bool belongsToUiLayer = false;
                        if (!string.IsNullOrEmpty(layerMember))
                        {
                            string[] members = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string m in members)
                            {
                                if (int.TryParse(m, out int idx) && idx == uiLayerIndex)
                                {
                                    belongsToUiLayer = true;
                                    break;
                                }
                            }
                        }

                        if (belongsToUiLayer)
                        {
                            // Apply a simple drop shadow
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                            shape.Fill.ShdwForegnd.Value = "#000000";          // Shadow color (black)
                            shape.Fill.ShdwForegndTrans.Value = 0.3;          // 30% transparency
                            shape.Fill.ShapeShdwOffsetX.Value = 0.1;          // Horizontal offset
                            shape.Fill.ShapeShdwOffsetY.Value = 0.1;          // Vertical offset
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
