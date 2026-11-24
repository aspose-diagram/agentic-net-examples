using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input file path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path (same folder, with suffix "_clean.vsdx")
        string directory = Path.GetDirectoryName(inputPath);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, fileNameWithoutExt + "_clean.vsdx");

        try
        {
            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Find the layer named "Obsolete" in the current page's layer collection
                    Aspose.Diagram.Layer targetLayer = null;
                    foreach (Aspose.Diagram.Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Obsolete")
                        {
                            targetLayer = layer;
                            break;
                        }
                    }

                    // If the layer does not exist on this page, continue to next page
                    if (targetLayer == null)
                        continue;

                    // Store the layer index (IX) as string for comparison with shape layer memberships
                    string layerIndexString = targetLayer.IX.ToString();

                    // Collect shapes that belong to the target layer
                    List<Aspose.Diagram.Shape> shapesToRemove = new List<Aspose.Diagram.Shape>();
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Retrieve the layer membership string (semicolon‑separated indexes)
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Split the membership string and check for the target layer index
                        string[] members = layerMember.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string member in members)
                        {
                            if (member == layerIndexString)
                            {
                                shapesToRemove.Add(shape);
                                break;
                            }
                        }
                    }

                    // Remove the collected shapes from the page
                    foreach (Aspose.Diagram.Shape shape in shapesToRemove)
                    {
                        page.Shapes.Remove(shape);
                    }

                    // Hide the layer (cannot physically remove from collection)
                    targetLayer.Visible.Value = BOOL.False;
                    targetLayer.Print.Value = BOOL.False;
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processed successfully. Output saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}