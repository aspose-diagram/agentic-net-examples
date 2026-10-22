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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [metadata]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string metadata = args.Length >= 3 ? args[2] : "Processed";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether the target layer was found
            bool layerFound = false;

            // Iterate through all pages to locate the 'Security' layer
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Compare layer name using the .Value wrapper
                    if (layer.Name.Value == "Security")
                    {
                        // Append custom metadata to the layer name using a delimiter
                        // Existing metadata (if any) is replaced
                        layer.Name.Value = $"Security|{metadata}";
                        layerFound = true;
                        // No need to continue searching once found on this page
                        break;
                    }
                }

                // If the layer was found on this page, continue to next page
                if (layerFound)
                    continue;
            }

            // If the 'Security' layer does not exist, create it on the first page
            if (!layerFound && diagram.Pages.Count > 0)
            {
                Page firstPage = diagram.Pages[0];
                Layer newLayer = new Layer();

                // Set layer properties
                newLayer.Name.Value = $"Security|{metadata}";
                newLayer.Visible.Value = BOOL.True;
                newLayer.Print.Value = BOOL.True;
                newLayer.Lock.Value = BOOL.False;
                newLayer.IsColorChecked = BOOL.False; // default color check state

                // Add the new layer to the page's layer collection
                firstPage.PageSheet.Layers.Add(newLayer);
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}