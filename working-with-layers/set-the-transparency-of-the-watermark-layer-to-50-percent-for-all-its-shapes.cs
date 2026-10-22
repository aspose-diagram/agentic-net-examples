using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Locate the layer named "Watermark" on the current page.
                    Layer watermarkLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Watermark")
                        {
                            watermarkLayer = layer;
                            break;
                        }
                    }

                    // If the Watermark layer does not exist on this page, skip it.
                    if (watermarkLayer == null)
                        continue;

                    // The layer index (IX) is used in the shape's layer membership string.
                    string watermarkIndex = watermarkLayer.IX.ToString();

                    // Iterate over all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (e.g., "0;2;5").
                        string layerMember = shape.LayerMem?.LayerMember?.Value ?? string.Empty;

                        // Determine if the shape belongs to the Watermark layer.
                        bool belongsToWatermark = false;
                        string[] members = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string member in members)
                        {
                            if (member == watermarkIndex)
                            {
                                belongsToWatermark = true;
                                break;
                            }
                        }

                        if (!belongsToWatermark)
                            continue;

                        // Set fill foreground transparency to 50% (0.5 = 50% transparent).
                        if (shape.Fill != null && shape.Fill.FillForegndTrans != null)
                            shape.Fill.FillForegndTrans.Value = 0.5;

                        // Set line color transparency to 50 percent.
                        if (shape.Line != null && shape.Line.LineColorTrans != null)
                            shape.Line.LineColorTrans.Value = 50;
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}