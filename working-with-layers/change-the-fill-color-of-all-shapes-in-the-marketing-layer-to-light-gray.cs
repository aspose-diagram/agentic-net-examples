using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
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
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Find the index of the layer named "Marketing" on the current page.
                    int marketingLayerIndex = -1;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare layer name using the .Value property of Str2Value.
                        if (layer.Name.Value == "Marketing")
                        {
                            // Layer.IX is an integer index (no .Value needed).
                            marketingLayerIndex = layer.IX;
                            break;
                        }
                    }

                    // If the page does not contain a "Marketing" layer, skip it.
                    if (marketingLayerIndex == -1)
                        continue;

                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (semicolon‑separated indexes).
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Determine whether the shape belongs to the Marketing layer.
                        bool belongsToMarketing = false;
                        string[] members = layerMember.Split(';');
                        foreach (string member in members)
                        {
                            if (int.TryParse(member, out int idx) && idx == marketingLayerIndex)
                            {
                                belongsToMarketing = true;
                                break;
                            }
                        }

                        if (!belongsToMarketing)
                            continue;

                        // Change the shape's fill to a solid light gray.
                        shape.Fill.FillPattern.Value = 1;               // Solid fill pattern.
                        shape.Fill.FillForegnd.Value = "#D3D3D3";        // Light gray color.
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}