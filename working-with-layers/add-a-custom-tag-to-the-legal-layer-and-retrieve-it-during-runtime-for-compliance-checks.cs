using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // The custom compliance tag to store on the 'Legal' layer
            string customTag = "Confidential";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the 'Legal' layer; the layer name may already contain a tag
            Layer legalLayer = null;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                // Extract the base name before any delimiter
                string baseName = layer.Name.Value.Split(';')[0];
                if (baseName == "Legal")
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
                legalLayer.IsColorChecked = BOOL.True; // Direct BOOL assignment, no .Value
                page.PageSheet.Layers.Add(legalLayer);
            }

            // Add or update the custom tag in the layer's name using a semicolon delimiter
            string[] nameParts = legalLayer.Name.Value.Split(';');
            bool tagUpdated = false;
            for (int i = 0; i < nameParts.Length; i++)
            {
                if (nameParts[i].StartsWith("Tag="))
                {
                    nameParts[i] = "Tag=" + customTag;
                    tagUpdated = true;
                    break;
                }
            }
            if (!tagUpdated)
            {
                var temp = new List<string>(nameParts) { "Tag=" + customTag };
                nameParts = temp.ToArray();
            }
            legalLayer.Name.Value = string.Join(";", nameParts);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // ----- Retrieval & compliance check -----
            string retrievedTag = null;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                string baseName = layer.Name.Value.Split(';')[0];
                if (baseName == "Legal")
                {
                    foreach (string part in layer.Name.Value.Split(';'))
                    {
                        if (part.StartsWith("Tag="))
                        {
                            retrievedTag = part.Substring("Tag=".Length);
                            break;
                        }
                    }
                    break;
                }
            }

            if (retrievedTag == null)
            {
                throw new Exception("Compliance tag not found on the 'Legal' layer.");
            }

            Console.WriteLine($"Compliance tag on 'Legal' layer: {retrievedTag}");

            // Example compliance validation
            if (retrievedTag != "Confidential")
            {
                throw new Exception("Compliance check failed: unexpected tag value.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
