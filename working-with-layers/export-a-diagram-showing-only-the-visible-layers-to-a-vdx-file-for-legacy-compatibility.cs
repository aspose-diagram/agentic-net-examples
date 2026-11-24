using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page and its layers
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Ensure non‑visible layers are explicitly set to false
                        if (layer.Visible.Value == BOOL.False)
                        {
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Save the diagram in VDX format (legacy compatibility)
                diagram.Save(outputPath, SaveFileFormat.Vdx);
                Console.WriteLine($"Diagram saved to {outputPath} with only visible layers.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}