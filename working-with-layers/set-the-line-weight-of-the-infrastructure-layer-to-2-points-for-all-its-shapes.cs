using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Find the index of the 'Infrastructure' layer on this page.
                    int infraLayerIndex = -1;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Infrastructure")
                        {
                            infraLayerIndex = layer.IX;
                            break;
                        }
                    }

                    // If the layer does not exist on this page, skip to next page.
                    if (infraLayerIndex == -1)
                        continue;

                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (semicolon‑separated indexes).
                        string member = shape.LayerMem?.LayerMember?.Value;
                        if (string.IsNullOrEmpty(member))
                            continue;

                        // Split the membership string and check if it contains the target layer index.
                        var indices = member.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (indices.Contains(infraLayerIndex.ToString()))
                        {
                            // Set line weight to 2 points (2/72 inches).
                            shape.Line.LineWeight.Value = 2.0 / 72.0;
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Line weight updated successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}