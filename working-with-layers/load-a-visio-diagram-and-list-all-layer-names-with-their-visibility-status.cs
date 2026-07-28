using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        // Assign the first argument to a variable.
        string visioPath = args[0];

        // Verify that the specified file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the provided file path.
            Diagram diagram = new Diagram(visioPath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Output the page name and ID for context.
                Console.WriteLine($"Page: {page.Name} (ID: {page.ID})");

                // Access the collection of layers defined on the page's sheet.
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Retrieve the layer's display name.
                    string layerName = layer.Name.Value;

                    // Determine visibility status using the BOOL enum.
                    string visibility = layer.Visible.Value == BOOL.True ? "Visible" : "Hidden";

                    // Output the layer name along with its visibility.
                    Console.WriteLine($"  Layer: {layerName}, Visibility: {visibility}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any exceptions that occur during loading or processing to the error stream.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}