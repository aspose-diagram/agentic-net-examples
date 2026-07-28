using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (or iterate pages if needed)
            Page page = diagram.Pages[0];

            // Create a new layer named 'Annotations'
            Layer layer = new Layer();
            layer.Name.Value = "Annotations";          // Set layer name
            layer.Visible.Value = BOOL.True;          // Make the layer visible
            layer.IsColorChecked = BOOL.True;         // Enable color for the layer
            layer.Color.Value = "#0000FF";            // Set layer color to blue (hex)

            // Add the new layer to the page's layer collection
            page.PageSheet.Layers.Add(layer);

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
