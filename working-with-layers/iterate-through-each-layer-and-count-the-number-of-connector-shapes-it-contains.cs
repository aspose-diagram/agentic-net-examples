using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Output basic page information
                    Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");

                    // Iterate through each layer on the current page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        int connectorCount = 0;

                        // Iterate through each shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Check if the shape is a connector (1‑D shape)
                            if (shape.OneD)
                            {
                                // Retrieve the layer membership string (semicolon‑separated list of layer indexes)
                                string layerMembership = shape.LayerMem?.LayerMember?.Value ?? string.Empty;

                                if (!string.IsNullOrEmpty(layerMembership))
                                {
                                    // Split the membership string into individual indexes
                                    string[] memberIndexes = layerMembership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                    // Compare each index with the current layer's IX (index)
                                    foreach (string idx in memberIndexes)
                                    {
                                        if (idx == layer.IX.ToString())
                                        {
                                            connectorCount++;
                                            break; // No need to check other indexes for this shape
                                        }
                                    }
                                }
                            }
                        }

                        // Output the count for the current layer
                        Console.WriteLine($"Layer '{layer.Name.Value}' (IX {layer.IX}) contains {connectorCount} connector shape(s).");
                    }

                    Console.WriteLine(); // Blank line between pages
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}