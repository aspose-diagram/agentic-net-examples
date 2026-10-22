using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each layer on the current page.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        int connectorCount = 0;

                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip non‑connector shapes.
                            if (!shape.OneD)
                                continue;

                            // Ensure the shape has layer membership information.
                            if (shape.LayerMem == null || string.IsNullOrEmpty(shape.LayerMem.LayerMember.Value))
                                continue;

                            // The LayerMember string contains a semicolon‑separated list of layer indexes.
                            string[] memberIndexes = shape.LayerMem.LayerMember.Value.Split(';');

                            // If the current layer's index appears in the list, count this connector.
                            foreach (string idxStr in memberIndexes)
                            {
                                if (idxStr == layer.IX.ToString())
                                {
                                    connectorCount++;
                                    break;
                                }
                            }
                        }

                        // Output the count for the current layer.
                        Console.WriteLine($"Page '{page.Name}', Layer '{layer.Name.Value}': {connectorCount} connector shape(s)");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
