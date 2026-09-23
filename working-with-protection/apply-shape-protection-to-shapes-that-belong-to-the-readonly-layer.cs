using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Find the index of the layer named "ReadOnly"
                int readOnlyLayerIndex = -1;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "ReadOnly")
                    {
                        readOnlyLayerIndex = layer.IX; // zero‑based index of the layer
                        break;
                    }
                }

                // If the layer does not exist on this page, skip to the next page
                if (readOnlyLayerIndex == -1)
                    continue;

                // Process each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked for deletion
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the layer membership string (e.g., "0;2;5")
                    string layerMember = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(layerMember))
                        continue;

                    // Determine if the shape belongs to the "ReadOnly" layer
                    string[] memberIndexes = layerMember.Split(';');
                    bool belongsToReadOnly = false;
                    foreach (string idxStr in memberIndexes)
                    {
                        if (int.TryParse(idxStr, out int idx) && idx == readOnlyLayerIndex)
                        {
                            belongsToReadOnly = true;
                            break;
                        }
                    }

                    if (!belongsToReadOnly)
                        continue;

                    // Apply protection to the shape
                    shape.Protection.LockMoveX.Value = BOOL.True;
                    shape.Protection.LockMoveY.Value = BOOL.True;
                    shape.Protection.LockWidth.Value = BOOL.True;
                    shape.Protection.LockHeight.Value = BOOL.True;
                    shape.Protection.LockRotate.Value = BOOL.True;
                    shape.Protection.LockVtxEdit.Value = BOOL.True;
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
