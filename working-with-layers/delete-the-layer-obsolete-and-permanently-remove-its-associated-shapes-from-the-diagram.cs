using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (replace with actual path as needed)
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Locate the layer named 'Obsolete' on the current page
                Layer obsoleteLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Obsolete")
                    {
                        obsoleteLayer = layer;
                        break;
                    }
                }

                // If the layer was not found, continue to the next page
                if (obsoleteLayer == null)
                    continue;

                // Determine the index of the obsolete layer (used in shape layer membership strings)
                string layerIndex = obsoleteLayer.IX.ToString();

                // Collect IDs of shapes that belong to the obsolete layer
                List<long> shapesToRemove = new List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has layer membership information
                    if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                    {
                        string memberString = shape.LayerMem.LayerMember.Value;
                        if (!string.IsNullOrEmpty(memberString))
                        {
                            // Split the semicolon‑separated list of layer indexes
                            string[] members = memberString.Split(';');
                            foreach (string m in members)
                            {
                                if (m == layerIndex)
                                {
                                    shapesToRemove.Add(shape.ID);
                                    break;
                                }
                            }
                        }
                    }
                }

                // Remove the collected shapes from the page
                foreach (long shapeId in shapesToRemove)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        page.Shapes.Remove(shape);
                    }
                }

                // Hide the obsolete layer (Aspose.Diagram does not provide a direct removal method)
                obsoleteLayer.Visible.Value = BOOL.False;
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}