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
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath>");
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
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume the first page contains the layers we need to modify.
                Page page = diagram.Pages[0];

                // Iterate over all layers on the page and disable the one named "Grid".
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Compare the layer name; if it matches "Grid", set its visibility to false.
                    if (layer.Name.Value == "Grid")
                    {
                        layer.Visible.Value = BOOL.False;
                        // Optionally also hide it from printing.
                        layer.Print.Value = BOOL.False;
                    }
                }

                // Save the modified diagram as VSDX.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}