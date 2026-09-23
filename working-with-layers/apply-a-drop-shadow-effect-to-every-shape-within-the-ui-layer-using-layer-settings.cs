using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Name of the target layer
                string targetLayerName = "UI";

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Find the layer with the specified name and get its index
                    int uiLayerIndex = -1;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == targetLayerName)
                        {
                            uiLayerIndex = layer.IX;
                            break;
                        }
                    }

                    // If the layer was not found on this page, skip to the next page
                    if (uiLayerIndex == -1)
                        continue;

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the layer membership string (e.g., "0;2;5")
                        string memberString = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(memberString))
                            continue;

                        // Check if the shape belongs to the target layer
                        bool belongsToTargetLayer = false;
                        string[] members = memberString.Split(';');
                        foreach (string member in members)
                        {
                            if (member == uiLayerIndex.ToString())
                            {
                                belongsToTargetLayer = true;
                                break;
                            }
                        }

                        if (!belongsToTargetLayer)
                            continue;

                        // Apply a simple drop shadow to the shape
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Enable simple shadow
                        shape.Fill.ShdwForegnd.Value = "#000000";                    // Shadow color (black)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30% transparency
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;                     // Horizontal offset
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;                     // Vertical offset
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }