using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Output page information
                    Console.WriteLine($"Page: {page.Name}");

                    // Iterate through each layer on the current page's PageSheet
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Retrieve layer name
                        string layerName = layer.Name.Value;

                        // Determine visibility status using BOOL enum
                        string visibility = layer.Visible.Value == BOOL.True ? "Visible" : "Hidden";

                        // Output layer details
                        Console.WriteLine($"  Layer: {layerName}, Visibility: {visibility}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}