using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file; can be passed as a command‑line argument
            string filePath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Iterate through each layer on the current page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    int layerIndex = layer.IX;
                    string layerIndexStr = layerIndex.ToString();
                    int shapeCount = 0;

                    // Count shapes that belong to this layer
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the layer membership string (e.g., "0;2;3")
                        string member = shape.LayerMem?.LayerMember?.Value;

                        if (!string.IsNullOrEmpty(member))
                        {
                            // Split the membership string and check for the current layer index
                            string[] parts = member.Split(';');
                            foreach (string part in parts)
                            {
                                if (part == layerIndexStr)
                                {
                                    shapeCount++;
                                    break;
                                }
                            }
                        }
                    }

                    // Determine visibility text
                    string visibility = layer.Visible.Value == BOOL.True ? "Visible" : "Hidden";

                    // Output the summary for this layer
                    Console.WriteLine($"Layer: {layer.Name.Value}, Visibility: {visibility}, Shape Count: {shapeCount}");
                }

                Console.WriteLine(); // Blank line between pages
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
