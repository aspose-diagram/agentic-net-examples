using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input and output file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool layerRenamed = false;

                // Iterate through each page because layers are stored per page sheet
                foreach (Page page in diagram.Pages)
                {
                    // Search for the layer named 'Background'
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Background")
                        {
                            // Store the original layer index before renaming
                            int oldLayerIndex = layer.IX;

                            // Rename the layer to 'Base'
                            layer.Name.Value = "Base";
                            layerRenamed = true;

                            // Update all shapes on this page that reference the old layer index
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Retrieve the layer membership string (semicolon‑separated indexes)
                                string membership = shape.LayerMem.LayerMember.Value;
                                if (string.IsNullOrEmpty(membership))
                                    continue;

                                // Split, check, and rebuild the membership string if needed
                                string[] parts = membership.Split(';');
                                bool changed = false;
                                for (int i = 0; i < parts.Length; i++)
                                {
                                    if (parts[i] == oldLayerIndex.ToString())
                                    {
                                        // Index remains the same; no actual change required,
                                        // but we mark it as processed for completeness
                                        changed = true;
                                    }
                                }

                                // Reassign the (potentially unchanged) membership string
                                if (changed)
                                    shape.LayerMem.LayerMember.Value = string.Join(";", parts);
                            }

                            // No need to continue searching after rename
                            break;
                        }
                    }
                }

                if (!layerRenamed)
                {
                    Console.WriteLine("Layer named 'Background' was not found in the document.");
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}