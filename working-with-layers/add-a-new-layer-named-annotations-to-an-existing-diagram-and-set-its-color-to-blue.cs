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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (layers are stored per page)
            Page page = diagram.Pages[0];

            // Create a new layer named "Annotations"
            Layer layer = new Layer();
            layer.Name.Value = "Annotations";
            layer.Visible.Value = BOOL.True;          // Make the layer visible
            layer.IsColorChecked = BOOL.True;         // Enable color for the layer
            layer.Color.Value = "#0000FF";            // Set layer color to blue (hex)

            // Add the new layer to the page's layer collection
            page.PageSheet.Layers.Add(layer);

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
