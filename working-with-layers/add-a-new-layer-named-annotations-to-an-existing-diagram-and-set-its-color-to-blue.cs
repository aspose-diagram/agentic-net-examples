using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the existing Visio diagram
            string inputPath = "input.vsdx";
            // Path where the modified diagram will be saved
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add the new layer
            foreach (Page page in diagram.Pages)
            {
                // Create a new layer instance
                Layer layer = new Layer();

                // Set the layer name
                layer.Name.Value = "Annotations";

                // Enable the color for the layer
                layer.IsColorChecked = BOOL.True;

                // Set the layer color to blue (hex format)
                layer.Color.Value = "#0000FF";

                // Add the layer to the page's layer collection
                page.PageSheet.Layers.Add(layer);
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
