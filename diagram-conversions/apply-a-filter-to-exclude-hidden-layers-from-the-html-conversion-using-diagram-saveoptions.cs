using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and their layers.
            // Hidden layers (Visible == BOOL.False) are automatically omitted during HTML export.
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // No action needed – the layer's visibility is respected by the exporter.
                    // Example of forcing a layer to be hidden (if required):
                    // if (layer.Name.Value == "SomeLayer") layer.Visible.Value = BOOL.False;
                }
            }

            // Configure HTML save options to exclude hidden pages (layers are handled by visibility)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                ExportHiddenPage = false // ExportHiddenPage expects a native bool, not BOOL
            };

            // Save the diagram as HTML using the configured options
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
