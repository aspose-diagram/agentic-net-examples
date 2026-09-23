using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Find the 'Infrastructure' layer on the current page
                Layer infrastructureLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Infrastructure")
                    {
                        infrastructureLayer = layer;
                        break;
                    }
                }

                // If the layer does not exist on this page, skip to the next page
                if (infrastructureLayer == null)
                    continue;

                // Get the index of the layer as a string (used in the layer membership cell)
                string layerIndexStr = infrastructureLayer.IX.ToString();

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape belongs to the 'Infrastructure' layer
                    // The LayerMember cell contains a semicolon‑separated list of layer indexes
                    string[] memberIndexes = shape.LayerMem.LayerMember.Value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (memberIndexes.Contains(layerIndexStr))
                    {
                        // Set line weight to 2 points (2/72 inches)
                        shape.Line.LineWeight.Value = 2.0 / 72.0;
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
