using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Custom tag to associate with the 'Legal' layer
                string customTag = "Compliance123";

                // Find the 'Legal' layer; if it doesn't exist, create it
                Layer legalLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Layer names are stored in the Name.Value property
                    if (layer.Name.Value.Split('|')[0] == "Legal")
                    {
                        legalLayer = layer;
                        break;
                    }
                }

                if (legalLayer == null)
                {
                    // Create a new layer named 'Legal'
                    legalLayer = new Layer();
                    legalLayer.Name.Value = "Legal";
                    legalLayer.Visible.Value = BOOL.True;
                    legalLayer.IsColorChecked = BOOL.True; // required enum assignment
                    page.PageSheet.Layers.Add(legalLayer);
                }

                // Append or update the custom tag in the layer's name using a pipe delimiter
                // Format: BaseName|Tag=YourTagValue
                string baseName = "Legal";
                legalLayer.Name.Value = $"{baseName}|Tag={customTag}";

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // ----- Runtime retrieval for compliance check -----
                // Reload the diagram (simulating a separate runtime operation)
                Diagram loadedDiagram = new Diagram(outputPath);
                Page loadedPage = loadedDiagram.Pages[0];

                // Retrieve the tag from the 'Legal' layer
                string retrievedTag = null;
                foreach (Layer layer in loadedPage.PageSheet.Layers)
                {
                    // Split the stored name to isolate the base name and any metadata
                    var nameParts = layer.Name.Value.Split('|');
                    if (nameParts[0] == "Legal")
                    {
                        // Look for a part that starts with "Tag="
                        var tagPart = nameParts.FirstOrDefault(p => p.StartsWith("Tag="));
                        if (tagPart != null)
                        {
                            retrievedTag = tagPart.Substring("Tag=".Length);
                        }
                        break;
                    }
                }

                // Perform compliance verification
                if (retrievedTag == null)
                {
                    throw new Exception("Compliance tag not found on the 'Legal' layer.");
                }

                if (retrievedTag != customTag)
                {
                    throw new Exception($"Compliance tag mismatch. Expected: {customTag}, Found: {retrievedTag}");
                }

                Console.WriteLine($"Compliance check passed. Tag value: {retrievedTag}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }