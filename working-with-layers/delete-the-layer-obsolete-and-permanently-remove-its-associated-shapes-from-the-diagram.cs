using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the diagram from the specified file
            diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Find the layer named 'Obsolete' in the current page's layer collection
                Layer targetLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Obsolete")
                    {
                        targetLayer = layer;
                        break;
                    }
                }

                // If the layer was not found on this page, continue to next page
                if (targetLayer == null)
                    continue;

                // Determine the string representation of the layer index
                string layerIndexStr = targetLayer.IX.ToString();

                // Collect shapes that belong to the 'Obsolete' layer
                List<Shape> shapesToRemove = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has layer membership information
                    if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                    {
                        string memberValue = shape.LayerMem.LayerMember.Value;
                        if (!string.IsNullOrEmpty(memberValue))
                        {
                            // Split the semicolon‑separated list of layer indexes
                            string[] indexes = memberValue.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string idx in indexes)
                            {
                                if (idx == layerIndexStr)
                                {
                                    shapesToRemove.Add(shape);
                                    break;
                                }
                            }
                        }
                    }
                }

                // Remove the collected shapes from the page
                foreach (Shape shape in shapesToRemove)
                {
                    page.Shapes.Remove(shape);
                }

                // Hide the layer by setting its visibility and print properties to FALSE
                targetLayer.Visible.Value = BOOL.False;
                targetLayer.Print.Value = BOOL.False;
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released
            if (diagram != null)
                diagram.Dispose();
        }
    }
}