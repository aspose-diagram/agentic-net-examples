using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            string dir = Path.GetDirectoryName(inputPath) ?? "";
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{nameWithoutExt}_reordered.vsdx");
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page to reorder layers
                foreach (Page page in diagram.Pages)
                {
                    // Access the layer collection via the page's PageSheet
                    LayerCollection layers = page.PageSheet.Layers;

                    // Find the layer named "Background"
                    Layer backgroundLayer = null;
                    foreach (Layer layer in layers)
                    {
                        if (layer.Name.Value == "Background")
                        {
                            backgroundLayer = layer;
                            break;
                        }
                    }

                    // If no background layer found, continue to next page
                    if (backgroundLayer == null)
                        continue;

                    // Create a copy of the background layer with the same properties
                    Layer newLayer = new Layer();
                    newLayer.Name.Value = backgroundLayer.Name.Value;
                    newLayer.Visible.Value = backgroundLayer.Visible.Value;
                    newLayer.Print.Value = backgroundLayer.Print.Value;
                    newLayer.Lock.Value = backgroundLayer.Lock.Value;
                    newLayer.IsColorChecked = backgroundLayer.IsColorChecked;
                    newLayer.Color.Value = backgroundLayer.Color.Value;

                    // Add the new layer to the collection
                    layers.Add(newLayer);

                    // Attempt to set the new layer's index to 0 (bottom of stack)
                    try
                    {
                        newLayer.IX = 0;
                    }
                    catch (Exception ex)
                    {
                        // If setting IX fails, log but continue
                        Console.Error.WriteLine($"Failed to set layer index for page '{page.Name}': {ex.Message}");
                    }

                    // Hide the original background layer
                    backgroundLayer.Visible.Value = BOOL.False;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or IO errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}