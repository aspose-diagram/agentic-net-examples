using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <layerName> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string targetLayerName = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and adjust layer visibility.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Show only the requested layer; hide all others.
                        if (string.Equals(layer.Name.Value, targetLayerName, StringComparison.OrdinalIgnoreCase))
                        {
                            layer.Visible.Value = BOOL.True;
                        }
                        else
                        {
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Configure PNG export options.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // TODO: If a TransparentBackground property becomes available, set it to BOOL.True here.
                saveOptions.Resolution = 300f;          // Optional: set DPI.
                saveOptions.PageIndex = 0;              // Export the first page.
                saveOptions.PageCount = 1;              // Export a single page.

                // Save the diagram as a PNG image.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Layer \"{targetLayerName}\" exported to PNG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}