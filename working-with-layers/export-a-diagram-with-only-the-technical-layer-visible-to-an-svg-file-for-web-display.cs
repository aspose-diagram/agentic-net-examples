using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputSvgFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path; if not provided, use same folder with .svg extension
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".svg");

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each layer in the current page's sheet
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Set visibility: only the layer named "Technical" stays visible
                        if (layer.Name.Value == "Technical")
                        {
                            layer.Visible.Value = BOOL.True;
                        }
                        else
                        {
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Export the diagram to SVG format
                diagram.Save(outputPath, SaveFileFormat.Svg);
            }

            Console.WriteLine($"Diagram exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}