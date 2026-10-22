using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first command‑line argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Output the page name
                Console.WriteLine($"Page: {page.Name}");

                // Iterate over each layer defined in the page's PageSheet
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Retrieve the layer's name
                    string layerName = layer.Name.Value;

                    // Determine visibility status using Aspose BOOL enum
                    string visibility = layer.Visible.Value == BOOL.True ? "Visible" : "Hidden";

                    // Print layer information
                    Console.WriteLine($"  Layer: {layerName}, Visibility: {visibility}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}