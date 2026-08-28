using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Locate the layer named "Obsolete"
                int obsoleteLayerIndex = -1;
                Layer obsoleteLayer = null;
                int currentIndex = 0;

                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Obsolete")
                    {
                        obsoleteLayerIndex = currentIndex;
                        obsoleteLayer = layer;
                        break;
                    }
                    currentIndex++;
                }

                // If the layer was not found, continue to the next page
                if (obsoleteLayerIndex == -1)
                    continue;

                // Collect shapes that belong to the "Obsolete" layer
                List<Shape> shapesToRemove = new List<Shape>();

                foreach (Shape shape in page.Shapes)
                {
                    string layerMember = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(layerMember))
                        continue;

                    // The LayerMember string contains semicolon‑separated layer indexes
                    string[] members = layerMember.Split(';');
                    foreach (string member in members)
                    {
                        if (int.TryParse(member, out int idx) && idx == obsoleteLayerIndex)
                        {
                            shapesToRemove.Add(shape);
                            break;
                        }
                    }
                }

                // Remove the collected shapes from the page
                foreach (Shape shape in shapesToRemove)
                {
                    page.Shapes.Remove(shape);
                }

                // Hide the layer (Aspose.Diagram does not provide a direct removal method)
                obsoleteLayer.Visible.Value = BOOL.False;
                obsoleteLayer.Print.Value = BOOL.False;
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
