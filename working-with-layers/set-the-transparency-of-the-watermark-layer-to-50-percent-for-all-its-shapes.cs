using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Find the 'Watermark' layer on the current page
                    Layer watermarkLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Watermark")
                        {
                            watermarkLayer = layer;
                            break;
                        }
                    }

                    // If the layer is not present on this page, skip it
                    if (watermarkLayer == null)
                        continue;

                    // Index of the watermark layer as string (e.g., "3")
                    string layerIndexStr = watermarkLayer.IX.ToString();

                    // Iterate over all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the layer membership string (e.g., "0;2;3")
                        string membership = shape.LayerMem?.LayerMember?.Value ?? string.Empty;

                        // Split into individual layer indexes
                        string[] members = membership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        // Determine if the shape belongs to the watermark layer
                        bool belongsToWatermark = false;
                        foreach (string member in members)
                        {
                            if (member == layerIndexStr)
                            {
                                belongsToWatermark = true;
                                break;
                            }
                        }

                        if (!belongsToWatermark)
                            continue;

                        // Set line transparency to 50%
                        shape.Line.LineColorTrans.Value = 50;

                        // Set fill foreground transparency to 50%
                        shape.Fill.FillForegndTrans.Value = 50;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}