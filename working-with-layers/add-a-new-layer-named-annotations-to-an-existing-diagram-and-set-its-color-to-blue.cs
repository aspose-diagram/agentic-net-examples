using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new layer named 'Annotations'.
                Layer newLayer = new Layer();
                newLayer.Name.Value = "Annotations";
                newLayer.Visible.Value = BOOL.True;
                newLayer.Print.Value = BOOL.True;
                // Enable color for the layer and set it to blue.
                newLayer.IsColorChecked = BOOL.True;
                newLayer.Color.Value = "#0000FF";

                // Add the new layer to each page's layer collection.
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.Layers.Add(newLayer);
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}