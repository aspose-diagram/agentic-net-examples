using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Path for the output Visio file after removal
        string outputPath = "output.vsdx";
        // Guard to ensure the output directory exists (optional, but prevents later errors)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir)) { Console.Error.WriteLine($"Output directory not found: {outputDir}"); return; }

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Name of the layer to delete
            const string targetLayerName = "Obsolete";

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Locate the layer with the specified name on the current page
                Layer layerToDelete = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Compare the layer's name value with the target name
                    if (layer.Name.Value == targetLayerName)
                    {
                        layerToDelete = layer;
                        break;
                    }
                }

                // If the layer does not exist on this page, continue to next page
                if (layerToDelete == null)
                    continue;

                // The layer index (as string) used in the shape's LayerMember property
                string layerIndexString = layerToDelete.IX.ToString();

                // Collect shapes that belong to the target layer
                System.Collections.Generic.List<Shape> shapesToRemove = new System.Collections.Generic.List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // LayerMember is a semicolon‑separated list of layer indexes
                    string member = shape.LayerMem.LayerMember.Value;
                    if (!string.IsNullOrEmpty(member) && member.Split(';').Contains(layerIndexString))
                    {
                        shapesToRemove.Add(shape);
                    }
                }

                // Remove the collected shapes from the page
                foreach (Shape shape in shapesToRemove)
                {
                    page.Shapes.Remove(shape);
                }

                // Finally, remove the layer itself from the page's layer collection
                page.PageSheet.Layers.Remove(layerToDelete);
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}