using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            bool layerFound = false;

            // Iterate through all pages and their layers to locate the 'Security' layer
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Compare layer name using the Str2Value wrapper
                    if (layer.Name.Value == "Security")
                    {
                        // Append custom metadata to the layer name using a pipe delimiter
                        // Existing name may already contain metadata; we simply append another segment
                        string existingName = layer.Name.Value;
                        string metadataSegment = "Meta=Downstream";

                        // Build the new name
                        string newName = existingName.Contains("|")
                            ? $"{existingName}|{metadataSegment}"
                            : $"{existingName}|{metadataSegment}";

                        // Apply the new name to the layer
                        layer.Name.Value = newName;

                        layerFound = true;
                        break; // Exit inner loop once the target layer is processed
                    }
                }

                if (layerFound)
                    break; // Exit outer loop if the layer has been found
            }

            if (!layerFound)
            {
                Console.Error.WriteLine("Layer 'Security' not found in the diagram.");
                return;
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Custom metadata applied to 'Security' layer and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}