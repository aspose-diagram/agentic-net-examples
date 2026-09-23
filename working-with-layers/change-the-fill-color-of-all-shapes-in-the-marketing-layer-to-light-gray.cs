using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path where the modified file will be saved
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the document
                foreach (Page page in diagram.Pages)
                {
                    // Locate the 'Marketing' layer on the current page
                    int marketingLayerIndex = -1;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Marketing")
                        {
                            marketingLayerIndex = layer.IX; // zero‑based index of the layer
                            break;
                        }
                    }

                    // If the layer does not exist on this page, skip to the next page
                    if (marketingLayerIndex == -1)
                        continue;

                    string targetIndexString = marketingLayerIndex.ToString();

                    // Process each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has layer membership information
                        if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                        {
                            string members = shape.LayerMem.LayerMember.Value;
                            if (string.IsNullOrEmpty(members))
                                continue;

                            // The LayerMember string contains semicolon‑separated layer indexes
                            string[] memberIndexes = members.Split(';');
                            foreach (string idx in memberIndexes)
                            {
                                if (idx == targetIndexString)
                                {
                                    // Apply a solid light‑gray fill to the shape
                                    shape.Fill.FillPattern.Value = 1;               // Solid fill
                                    shape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray (hex)
                                    break;
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }