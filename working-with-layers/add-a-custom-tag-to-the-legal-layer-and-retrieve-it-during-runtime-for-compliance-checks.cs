using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, custom tag value
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <customTag>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string customTag = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page (layers are page‑specific)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Aspose.Diagram.Page page = diagram.Pages[0];

            // Search for an existing layer whose base name is "Legal"
            Layer targetLayer = null;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                // Layer.Name.Value holds the full name (may contain our custom tag)
                string fullName = layer.Name.Value;
                // Base name is the part before any delimiter '|'
                string baseName = fullName.Split('|')[0];
                if (string.Equals(baseName, "Legal", StringComparison.OrdinalIgnoreCase))
                {
                    targetLayer = layer;
                    break;
                }
            }

            // If the "Legal" layer does not exist, create it
            if (targetLayer == null)
            {
                targetLayer = new Layer();
                targetLayer.Name.Value = "Legal"; // initial name without tag
                targetLayer.Visible.Value = BOOL.True;
                targetLayer.Print.Value = BOOL.True;
                targetLayer.Lock.Value = BOOL.False;
                targetLayer.IsColorChecked = BOOL.False;
                // Add the new layer to the collection
                page.PageSheet.Layers.Add(targetLayer);
            }

            // Embed the custom tag into the layer name using a delimiter
            // Format: BaseName|Tag=customTag
            string newFullName = $"Legal|Tag={customTag}";
            targetLayer.Name.Value = newFullName;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");

            // ----- Runtime retrieval for compliance check -----
            // Re‑load the saved diagram (simulating a separate runtime session)
            Diagram loadedDiagram = new Diagram(outputPath);
            Aspose.Diagram.Page loadedPage = loadedDiagram.Pages[0];
            string retrievedTag = null;

            foreach (Layer layer in loadedPage.PageSheet.Layers)
            {
                string fullName = layer.Name.Value;
                string[] parts = fullName.Split('|');
                if (parts.Length > 0 && string.Equals(parts[0], "Legal", StringComparison.OrdinalIgnoreCase))
                {
                    // Look for a part that starts with "Tag="
                    foreach (string part in parts)
                    {
                        if (part.StartsWith("Tag=", StringComparison.OrdinalIgnoreCase))
                        {
                            retrievedTag = part.Substring(4); // extract value after "Tag="
                            break;
                        }
                    }
                    break;
                }
            }

            if (retrievedTag != null)
            {
                Console.WriteLine($"Compliance tag retrieved from 'Legal' layer: {retrievedTag}");
            }
            else
            {
                Console.WriteLine("Compliance tag not found on the 'Legal' layer.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}