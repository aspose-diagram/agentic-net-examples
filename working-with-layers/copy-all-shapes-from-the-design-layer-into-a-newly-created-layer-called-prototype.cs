using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume layers are stored in the first page's PageSheet
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page firstPage = diagram.Pages[0];
                Layer designLayer = null;

                // Locate the 'Design' layer
                foreach (Layer layer in firstPage.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Design")
                    {
                        designLayer = layer;
                        break;
                    }
                }

                if (designLayer == null)
                {
                    Console.Error.WriteLine("Layer named 'Design' was not found.");
                    return;
                }

                // Create the new 'Prototype' layer
                Layer prototypeLayer = new Layer();
                prototypeLayer.Name.Value = "Prototype";
                prototypeLayer.Visible.Value = BOOL.True;
                prototypeLayer.Print.Value = BOOL.True;
                prototypeLayer.Lock.Value = BOOL.False;
                prototypeLayer.IsColorChecked = BOOL.False;
                // Default color (black) – format "R,G,B"
                prototypeLayer.Color.Value = "0,0,0";

                // Add the new layer to the page's layer collection
                firstPage.PageSheet.Layers.Add(prototypeLayer);

                // Retrieve the index strings for both layers
                string designLayerIndex = designLayer.IX.ToString();
                string prototypeLayerIndex = prototypeLayer.IX.ToString();

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get current layer membership string (semicolon‑separated indices)
                        string currentMembers = shape.LayerMem.LayerMember.Value ?? string.Empty;

                        // Determine if the shape belongs to the Design layer
                        bool belongsToDesign = false;
                        if (!string.IsNullOrEmpty(currentMembers))
                        {
                            // Split the membership string and check for design layer index
                            string[] parts = currentMembers.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string part in parts)
                            {
                                if (part == designLayerIndex)
                                {
                                    belongsToDesign = true;
                                    break;
                                }
                            }
                        }

                        if (belongsToDesign)
                        {
                            // Add the Prototype layer index if not already present
                            if (!currentMembers.Contains(prototypeLayerIndex))
                            {
                                string newMembers = string.IsNullOrEmpty(currentMembers)
                                    ? prototypeLayerIndex
                                    : currentMembers + ";" + prototypeLayerIndex;

                                shape.LayerMem.LayerMember.Value = newMembers;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}