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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (you can adjust as needed).
                Page page = diagram.Pages[0];

                // Create a new layer named 'Annotations'.
                Layer newLayer = new Layer();
                newLayer.Name.Value = "Annotations";          // Set layer name.
                newLayer.Visible.Value = BOOL.True;           // Make the layer visible.
                newLayer.Print.Value = BOOL.True;             // Allow printing.
                newLayer.Lock.Value = BOOL.False;             // Ensure the layer is not locked.

                // Enable color for the layer and set it to blue.
                newLayer.IsColorChecked = BOOL.True;          // Activate color usage.
                newLayer.Color.Value = "#0000FF";             // Blue color in hex.

                // Add the new layer to the page's layer collection.
                page.PageSheet.Layers.Add(newLayer);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Layer 'Annotations' added and saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}