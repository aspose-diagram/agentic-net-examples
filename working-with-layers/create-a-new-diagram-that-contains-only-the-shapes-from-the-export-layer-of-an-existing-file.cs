using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string inputPath = "input.vsdx";
                string outputPath = "ExportLayerOnly.vsdx";

                // Load the source diagram
                Diagram srcDiagram = new Diagram(inputPath);

                // Assume we work with the first page (modify if needed)
                Page srcPage = srcDiagram.Pages[0];

                // Locate the 'Export' layer
                Layer exportLayer = null;
                foreach (Layer layer in srcPage.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Export")
                    {
                        exportLayer = layer;
                        break;
                    }
                }

                if (exportLayer == null)
                {
                    Console.WriteLine("Layer named 'Export' not found.");
                    return;
                }

                // Create a new empty diagram
                Diagram newDiagram = new Diagram();

                // Ensure the new diagram has at least one page
                Page newPage = newDiagram.Pages[0];

                // Copy all masters from source to target (required for shape creation)
                foreach (Master srcMaster in srcDiagram.Masters)
                {
                    newDiagram.Masters.Add(srcMaster);
                }

                // Helper to check if a shape belongs to the Export layer
                bool ShapeInExportLayer(Shape shape)
                {
                    string member = shape.LayerMem?.LayerMember?.Value;
                    if (string.IsNullOrEmpty(member))
                        return false;

                    string[] indices = member.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string idx in indices)
                    {
                        if (int.TryParse(idx, out int layerIdx) && layerIdx == exportLayer.IX)
                            return true;
                    }
                    return false;
                }

                // Iterate over shapes and copy those that belong to the Export layer
                foreach (Shape srcShape in srcPage.Shapes)
                {
                    if (!ShapeInExportLayer(srcShape))
                        continue;

                    // Ensure the shape has a master (skip if missing)
                    if (srcShape.Master == null)
                        continue;

                    string masterName = srcShape.Master.Name;

                    // Retrieve geometry
                    double pinX = srcShape.XForm.PinX.Value;
                    double pinY = srcShape.XForm.PinY.Value;
                    double width = srcShape.XForm.Width.Value;
                    double height = srcShape.XForm.Height.Value;

                    // Add shape to the new page
                    long newShapeId = newPage.AddShape(pinX, pinY, width, height, masterName, false);
                    Shape newShape = newPage.Shapes.GetShape(newShapeId);

                    // Copy plain text (if any)
                    newShape.Text.Value.Clear();
                    foreach (var fmt in srcShape.Text.Value)
                    {
                        if (fmt is Txt txt)
                        {
                            newShape.Text.Value.Add(new Txt(txt.Text));
                        }
                    }

                    // Assign the shape to the Export layer in the new diagram
                    newShape.LayerMem.LayerMember.Value = exportLayer.IX.ToString();
                }

                // Save the new diagram containing only Export-layer shapes
                newDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Export-layer diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }