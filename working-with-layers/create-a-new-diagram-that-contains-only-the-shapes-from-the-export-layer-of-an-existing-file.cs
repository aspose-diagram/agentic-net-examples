using System.IO;
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
            string sourcePath = "input.vsdx";

            // Load the existing diagram
            Diagram srcDiagram = new Diagram(sourcePath);

            // Create a new empty diagram
            Diagram newDiagram = new Diagram();

            // Copy all masters from the source diagram to the new diagram
            foreach (Master srcMaster in srcDiagram.Masters)
            {
                // Masters collection supports Add method
                newDiagram.Masters.Add(srcMaster);
            }

            // Assume we work with the first page of the new diagram (default page)
            Page targetPage = newDiagram.Pages[0];

            // Find the index of the layer named "Export" in each source page
            foreach (Page srcPage in srcDiagram.Pages)
            {
                // Locate the Export layer on the current source page
                int exportLayerIndex = -1;
                foreach (Layer layer in srcPage.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Export")
                    {
                        exportLayerIndex = layer.IX;
                        break;
                    }
                }

                // If the Export layer does not exist on this page, skip it
                if (exportLayerIndex == -1)
                    continue;

                // Iterate through all shapes on the source page
                foreach (Shape srcShape in srcPage.Shapes)
                {
                    // Retrieve the layer membership string (e.g., "0;2;5")
                    string layerMember = srcShape.LayerMem.LayerMember.Value ?? string.Empty;

                    // Split into individual indexes
                    string[] memberIndexes = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    // Check if the shape belongs to the Export layer
                    bool belongsToExport = false;
                    foreach (string idx in memberIndexes)
                    {
                        if (int.TryParse(idx, out int intIdx) && intIdx == exportLayerIndex)
                        {
                            belongsToExport = true;
                            break;
                        }
                    }

                    if (!belongsToExport)
                        continue; // Skip shapes not on the Export layer

                    // Ensure the shape has a master (required for AddShape overload)
                    if (srcShape.Master == null)
                        continue;

                    // Add the shape to the new diagram using the overload that copies the shape
                    long newShapeId = newDiagram.AddShape(srcShape, srcShape.Master.Name, 0);

                    // Retrieve the newly added shape (optional: further adjustments can be made here)
                    Shape newShape = targetPage.Shapes.GetShape(newShapeId);
                    // Example: preserve the shape's name
                    newShape.Name = srcShape.Name;
                    newShape.NameU = srcShape.NameU;
                }
            }

            // Save the new diagram containing only the Export layer shapes
            string outputPath = "ExportLayerOnly.vsdx";
            newDiagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
