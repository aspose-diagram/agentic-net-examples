using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate if the target layer was found
            bool layerFound = false;

            // Iterate through all pages and their layers to locate the 'Security' layer
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Security")
                    {
                        // Example modification: ensure the layer is visible
                        layer.Visible.Value = BOOL.True;

                        // Create a custom document property to hold metadata for the Security layer
                        var customProp = new CustomProp();
                        customProp.Name = "SecurityLayerMeta";
                        customProp.PropType = PropType.String;
                        customProp.CustomValue.ValueString = "Processed";

                        // Add the custom property to the diagram's custom properties collection
                        diagram.DocumentProps.CustomProps.Add(customProp);

                        layerFound = true;
                        break;
                    }
                }
                if (layerFound) break;
            }

            if (!layerFound)
            {
                Console.WriteLine("Security layer not found in the diagram.");
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
