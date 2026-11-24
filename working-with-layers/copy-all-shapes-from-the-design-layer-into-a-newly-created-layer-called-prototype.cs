using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input and output file paths.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
                // Iterate through each page because layers are stored per page.
                foreach (Page page in diagram.Pages)
                {
                    // Locate the 'Design' layer in the current page's layer collection.
                    Layer designLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Design")
                        {
                            designLayer = layer;
                            break;
                        }
                    }

                    if (designLayer == null)
                    {
                        Console.Error.WriteLine("Design layer not found on a page. Skipping this page.");
                        continue;
                    }

                    // Create a new layer named 'Prototype' and add it to the page.
                    Layer prototypeLayer = new Layer();
                    prototypeLayer.Name.Value = "Prototype";
                    // Set default visibility and print settings.
                    prototypeLayer.Visible.Value = BOOL.True;
                    prototypeLayer.Print.Value = BOOL.True;
                    prototypeLayer.Lock.Value = BOOL.False;
                    prototypeLayer.IsColorChecked = BOOL.False;
                    // Add the new layer to the page's layer collection.
                    page.PageSheet.Layers.Add(prototypeLayer);

                    // Retrieve the index (IX) of the newly added prototype layer as string.
                    string prototypeLayerIndex = prototypeLayer.IX.ToString();

                    // Iterate over all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve current layer membership string.
                        string currentMembership = shape.LayerMem.LayerMember.Value ?? string.Empty;

                        // Determine if the shape belongs to the Design layer.
                        // LayerMember stores semicolon‑separated layer indexes.
                        bool belongsToDesign = false;
                        if (!string.IsNullOrEmpty(currentMembership))
                        {
                            string[] members = currentMembership.Split(';');
                            foreach (string member in members)
                            {
                                if (member == designLayer.IX.ToString())
                                {
                                    belongsToDesign = true;
                                    break;
                                }
                            }
                        }

                        // If the shape is on the Design layer, add it to the Prototype layer.
                        if (belongsToDesign)
                        {
                            // Avoid duplicate addition of the prototype layer index.
                            bool alreadyInPrototype = false;
                            if (!string.IsNullOrEmpty(currentMembership))
                            {
                                string[] members = currentMembership.Split(';');
                                foreach (string member in members)
                                {
                                    if (member == prototypeLayerIndex)
                                    {
                                        alreadyInPrototype = true;
                                        break;
                                    }
                                }
                            }

                            // Append the prototype layer index if not already present.
                            if (!alreadyInPrototype)
                            {
                                string newMembership = string.IsNullOrEmpty(currentMembership)
                                    ? prototypeLayerIndex
                                    : currentMembership + ";" + prototypeLayerIndex;
                                shape.LayerMem.LayerMember.Value = newMembership;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}