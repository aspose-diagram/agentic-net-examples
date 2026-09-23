using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to save the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the diagram has at least one page
            Page page = diagram.Pages[0];

            // Find the 'Legal' layer (case‑sensitive)
            Layer legalLayer = null;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == "Legal")
                {
                    legalLayer = layer;
                    break;
                }
            }

            // If the layer does not exist, create it
            if (legalLayer == null)
            {
                legalLayer = new Layer();
                legalLayer.Name.Value = "Legal";
                legalLayer.Visible.Value = BOOL.True;
                page.PageSheet.Layers.Add(legalLayer);
            }

            // ----- Add a custom tag to the layer -----
            // Since Aspose.Diagram does not support a Tag collection,
            // we embed the tag in the layer name using a delimiter.
            // Example format: "Legal|Tag=ComplianceCheck"
            const string tagKey = "Tag";
            const string tagValue = "ComplianceCheck";

            // Check if the tag is already present
            string[] parts = legalLayer.Name.Value.Split('|');
            bool tagExists = false;
            foreach (string part in parts)
            {
                if (part.StartsWith(tagKey + "=", StringComparison.Ordinal))
                {
                    tagExists = true;
                    break;
                }
            }

            // Append the tag if it is missing
            if (!tagExists)
            {
                // Preserve existing name (first part) and add the tag
                string baseName = parts[0]; // should be "Legal"
                legalLayer.Name.Value = $"{baseName}|{tagKey}={tagValue}";
            }

            // ----- Retrieve the custom tag at runtime -----
            // Parse the layer name to extract the tag value
            string retrievedTag = null;
            parts = legalLayer.Name.Value.Split('|');
            foreach (string part in parts)
            {
                if (part.StartsWith(tagKey + "=", StringComparison.Ordinal))
                {
                    retrievedTag = part.Substring(tagKey.Length + 1); // value after "Tag="
                    break;
                }
            }

            // Perform a simple compliance check
            if (retrievedTag == tagValue)
            {
                Console.WriteLine("Compliance tag found on 'Legal' layer: " + retrievedTag);
            }
            else
            {
                throw new Exception("Compliance tag missing or incorrect on 'Legal' layer.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
