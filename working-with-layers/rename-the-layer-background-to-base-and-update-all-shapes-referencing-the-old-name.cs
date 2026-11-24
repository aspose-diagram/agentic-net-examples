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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Store old layer index for later shape updates
                int oldLayerIndex = -1;
                bool layerFound = false;

                // Iterate through all pages to locate the layer named "Background"
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Background")
                        {
                            // Capture the original index before renaming
                            oldLayerIndex = layer.IX;
                            // Rename the layer to "Base"
                            layer.Name.Value = "Base";
                            layerFound = true;
                            // No need to continue searching on this page
                            break;
                        }
                    }
                    if (layerFound) break;
                }

                if (!layerFound)
                {
                    Console.Error.WriteLine("Layer named 'Background' was not found in the document.");
                }
                else
                {
                    // Update shapes that reference the old layer index (if any custom handling is needed)
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True) continue;

                            // Retrieve current layer membership string (semicolon‑separated indexes)
                            string membership = shape.LayerMem.LayerMember.Value;

                            // If the shape references the old layer index, ensure the membership string remains valid.
                            // Since the index does not change, no modification is required, but this block
                            // demonstrates where custom updates would be applied if needed.
                            if (!string.IsNullOrEmpty(membership))
                            {
                                // Example placeholder: could replace old index with new if index changed.
                                // Here we simply leave the membership unchanged.
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}