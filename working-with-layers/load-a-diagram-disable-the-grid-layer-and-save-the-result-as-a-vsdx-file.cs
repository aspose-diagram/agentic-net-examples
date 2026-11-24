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
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages because layers are page‑specific.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Explicitly type the iterator as Page.
                    Page page = diagram.Pages[i];

                    // Iterate through the layer collection of the current page.
                    for (int j = 0; j < page.PageSheet.Layers.Count; j++)
                    {
                        Layer layer = page.PageSheet.Layers[j];

                        // Check if the layer name matches "Grid".
                        if (layer.Name != null && layer.Name.Value == "Grid")
                        {
                            // Disable the layer by setting its Visible cell to FALSE.
                            layer.Visible.Value = BOOL.False;
                            // Optionally also hide it from printing.
                            layer.Print.Value = BOOL.False;
                        }
                    }
                }

                // Save the modified diagram as VSDX using the correct SaveFileFormat enum.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}