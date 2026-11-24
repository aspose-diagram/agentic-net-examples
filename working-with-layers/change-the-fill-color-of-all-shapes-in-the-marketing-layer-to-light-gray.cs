using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Find the index of the 'Marketing' layer (assumed consistent across pages).
                int marketingLayerIndex = -1;
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];
                    foreach (Layer layer in firstPage.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Marketing")
                        {
                            marketingLayerIndex = layer.IX;
                            break;
                        }
                    }
                }

                // If the layer was not found, report and exit.
                if (marketingLayerIndex == -1)
                {
                    Console.Error.WriteLine("Layer 'Marketing' not found in the diagram.");
                    return;
                }

                // Iterate through all pages and their shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the shape's layer membership string.
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Check if the shape belongs to the Marketing layer.
                        string[] members = layerMember.Split(';');
                        foreach (string member in members)
                        {
                            if (int.TryParse(member, out int idx) && idx == marketingLayerIndex)
                            {
                                // Apply a solid light gray fill to the shape.
                                shape.Fill.FillPattern.Value = 1;               // Solid fill pattern.
                                shape.Fill.FillForegnd.Value = "#D3D3D3";        // Light gray color.
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}