using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio diagram file: ");
            inputPath = Console.ReadLine();
        }

        // Guard against missing file
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Store layer information: key = layer index (IX)
                var layerInfo = new Dictionary<int, (string Name, BOOL Visible, int ShapeCount)>();

                // First pass: collect all layers from every page
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has a PageSheet and Layers collection
                    if (page.PageSheet?.Layers == null) continue;

                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        int ix = layer.IX;
                        if (!layerInfo.ContainsKey(ix))
                        {
                            // layer.Name is a Str2Value; use .Value to get the string
                            string layerName = layer.Name.Value;
                            // layer.Visible is a BOOL wrapper; use .Value to get the BOOL enum
                            BOOL visibility = layer.Visible.Value;
                            layerInfo[ix] = (layerName, visibility, 0);
                        }
                    }
                }

                // Second pass: count shapes assigned to each layer
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True) continue;

                        // Retrieve the layer membership string (e.g., "1,3" or "1;3")
                        string memberValue = shape.LayerMem?.LayerMember?.Value;
                        if (string.IsNullOrWhiteSpace(memberValue))
                            continue;

                        // Split by commas or semicolons
                        char[] separators = new char[] { ',', ';' };
                        string[] parts = memberValue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part.Trim(), out int layerIx))
                            {
                                if (layerInfo.ContainsKey(layerIx))
                                {
                                    var info = layerInfo[layerIx];
                                    info.ShapeCount += 1;
                                    layerInfo[layerIx] = info;
                                }
                            }
                        }
                    }
                }

                // Output the summary report
                Console.WriteLine("Layer Summary Report");
                Console.WriteLine(new string('-', 45));
                Console.WriteLine("{0,-30} {1,-10} {2,10}", "Layer Name", "Visible", "Shape Count");
                Console.WriteLine(new string('-', 45));

                foreach (var kvp in layerInfo)
                {
                    var info = kvp.Value;
                    string visibility = info.Visible == BOOL.True ? "True" : "False";
                    Console.WriteLine("{0,-30} {1,-10} {2,10}", info.Name, visibility, info.ShapeCount);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}