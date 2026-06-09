using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (empty Visio document)
        Diagram diagram = new Diagram();

        // Ensure there is at least one page
        if (diagram.Pages.Count == 0)
        {
            // Add a default page if none exist
            diagram.Pages.Add(new Page());
        }

        // Work with the first page
        Page page = diagram.Pages[0];

        // Define the custom tag to store
        string customTag = "ComplianceTag=Confidential";

        // Search for an existing layer named "Legal"
        Layer targetLayer = null;
        foreach (Layer layer in page.PageSheet.Layers)
        {
            // Layer.Name is a Str2Value wrapper; compare its .Value
            if (layer.Name.Value != null && layer.Name.Value.StartsWith("Legal", StringComparison.OrdinalIgnoreCase))
            {
                targetLayer = layer;
                break;
            }
        }

        if (targetLayer == null)
        {
            // Layer not found – create a new one
            targetLayer = new Layer();
            // Store the name with the custom tag appended using a delimiter
            targetLayer.Name.Value = $"Legal|{customTag}";
            // Set visibility (optional, using BOOL enum)
            targetLayer.Visible.Value = BOOL.True;
            // Add the new layer to the page's layer collection
            page.PageSheet.Layers.Add(targetLayer);
        }
        else
        {
            // Layer exists – ensure the custom tag is present in the name
            string name = targetLayer.Name.Value;
            if (!name.Contains(customTag))
            {
                // Append the tag using the same delimiter
                targetLayer.Name.Value = $"{name}|{customTag}";
            }
        }

        // Save the diagram to a file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        // ----- Runtime retrieval for compliance check -----
        Console.WriteLine("Retrieving custom tag from 'Legal' layer...");

        // Re-open the saved diagram (simulating a separate runtime session)
        Diagram loadedDiagram = new Diagram("output.vsdx");
        Page loadedPage = loadedDiagram.Pages[0];

        // Locate the 'Legal' layer again
        Layer legalLayer = null;
        foreach (Layer layer in loadedPage.PageSheet.Layers)
        {
            if (layer.Name.Value != null && layer.Name.Value.StartsWith("Legal", StringComparison.OrdinalIgnoreCase))
            {
                legalLayer = layer;
                break;
            }
        }

        if (legalLayer != null)
        {
            // Extract the tag part after the delimiter '|'
            string[] parts = legalLayer.Name.Value.Split('|');
            string tagValue = null;
            foreach (string part in parts)
            {
                if (part.StartsWith("ComplianceTag=", StringComparison.OrdinalIgnoreCase))
                {
                    tagValue = part.Substring("ComplianceTag=".Length);
                    break;
                }
            }

            if (tagValue != null)
            {
                Console.WriteLine($"Compliance tag found: {tagValue}");
            }
            else
            {
                Console.WriteLine("Compliance tag not found on the 'Legal' layer.");
            }
        }
        else
        {
            Console.WriteLine("Layer named 'Legal' was not found.");
        }
    }
}
