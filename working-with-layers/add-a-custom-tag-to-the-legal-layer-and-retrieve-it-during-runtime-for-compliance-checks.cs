using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-visio-file>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Define the custom tag we want to associate with the 'Legal' layer.
        const string customTag = "Compliance";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages to locate (or create) the 'Legal' layer.
            foreach (Page page in diagram.Pages)
            {
                Layer targetLayer = null;

                // Search for an existing layer named exactly "Legal" (ignoring any existing tag suffix).
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // The layer name may already contain a tag after a '|' delimiter.
                    string baseName = layer.Name.Value.Split('|')[0];
                    if (baseName.Equals("Legal", StringComparison.OrdinalIgnoreCase))
                    {
                        targetLayer = layer;
                        break;
                    }
                }

                // If the layer does not exist, create it.
                if (targetLayer == null)
                {
                    targetLayer = new Layer();
                    targetLayer.Name.Value = "Legal";
                    targetLayer.Visible.Value = BOOL.True;
                    targetLayer.Print.Value = BOOL.True;
                    targetLayer.Lock.Value = BOOL.False;
                    targetLayer.IsColorChecked = BOOL.False;
                    // Add the new layer to the page's layer collection.
                    page.PageSheet.Layers.Add(targetLayer);
                }

                // Attach or update the custom tag using a '|' delimiter in the layer name.
                // Example final name: "Legal|Compliance"
                string[] parts = targetLayer.Name.Value.Split('|');
                if (parts.Length == 1)
                {
                    // No tag present; append it.
                    targetLayer.Name.Value = $"{parts[0]}|{customTag}";
                }
                else
                {
                    // Tag already present; replace with the new tag.
                    targetLayer.Name.Value = $"{parts[0]}|{customTag}";
                }

                // Retrieve and display the tag for verification.
                string[] retrievedParts = targetLayer.Name.Value.Split('|');
                string retrievedTag = retrievedParts.Length > 1 ? retrievedParts[1] : string.Empty;
                Console.WriteLine($"Page {page.Name}: Layer '{retrievedParts[0]}' has tag '{retrievedTag}'.");
            }

            // Save the modified diagram to a new file.
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Modified_" + Path.GetFileName(inputPath));
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with updated layer tag to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}