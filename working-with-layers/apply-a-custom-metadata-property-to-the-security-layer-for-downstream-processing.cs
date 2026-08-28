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
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the metadata to attach
            string metadataKey = "CustomMeta";
            string metadataValue = "DownstreamProcessing";

            // Iterate through all pages to find the 'Security' layer
            foreach (Page page in diagram.Pages)
            {
                // Access the layer collection via the page's sheet
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Compare the layer name (Str2Value) with the target name
                    if (layer.Name.Value == "Security")
                    {
                        // Append metadata to the layer name using a delimiter
                        // Example format: Security|CustomMeta=DownstreamProcessing
                        layer.Name.Value = $"Security|{metadataKey}={metadataValue}";
                    }
                }
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
