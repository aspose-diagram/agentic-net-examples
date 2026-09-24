using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output DOT file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "hierarchy.dot";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a writer for the DOT graph file
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("digraph G {");

                int pageIndex = 0;
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    pageIndex++;

                    // Iterate through all layers on the current page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        string layerName = layer.Name.Value;
                        int layerIndex = layer.IX;

                        // Begin a subgraph (cluster) for the layer
                        writer.WriteLine($"  subgraph cluster_{pageIndex}_{layerIndex} {{");
                        writer.WriteLine($"    label = \"{layerName}\";");

                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Get the layer membership string (e.g., "0;2")
                            string memberString = shape.LayerMem.LayerMember.Value;
                            if (string.IsNullOrEmpty(memberString))
                                continue;

                            // Check if the shape belongs to the current layer
                            string[] members = memberString.Split(';');
                            foreach (string member in members)
                            {
                                if (int.TryParse(member, out int memberIdx) && memberIdx == layerIndex)
                                {
                                    string shapeId = shape.ID.ToString();
                                    string shapeLabel = shape.NameU; // Shape name (universal)

                                    // Write a node for the shape inside the layer cluster
                                    writer.WriteLine($"    shape_{pageIndex}_{shapeId} [label=\"{shapeLabel}\"];");
                                    break;
                                }
                            }
                        }

                        // End the subgraph for this layer
                        writer.WriteLine("  }");
                    }
                }

                writer.WriteLine("}");
            }

            Console.WriteLine($"Layer hierarchy exported to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}