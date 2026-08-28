using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Locate the layer named "ReadOnly" on the current page
                Layer readOnlyLayer = null;
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "ReadOnly")
                    {
                        readOnlyLayer = layer;
                        break;
                    }
                }

                // If the layer does not exist on this page, skip to the next page
                if (readOnlyLayer == null)
                    continue;

                // The layer index as a string (used in the shape's layer membership string)
                string layerIndexStr = readOnlyLayer.IX.ToString();

                // Process each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the layer membership string (e.g., "0;2;5")
                    string member = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(member))
                        continue;

                    // Determine if the shape belongs to the "ReadOnly" layer
                    var indices = member.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (!indices.Contains(layerIndexStr))
                        continue;

                    // Apply full protection to the shape
                    shape.Protection.LockAspect.Value = BOOL.True;
                    shape.Protection.LockBegin.Value = BOOL.True;
                    shape.Protection.LockCalcWH.Value = BOOL.True;
                    shape.Protection.LockCrop.Value = BOOL.True;
                    shape.Protection.LockCustProp.Value = BOOL.True;
                    shape.Protection.LockDelete.Value = BOOL.True;
                    shape.Protection.LockEnd.Value = BOOL.True;
                    shape.Protection.LockFormat.Value = BOOL.True;
                    shape.Protection.LockFromGroupFormat.Value = BOOL.True;
                    shape.Protection.LockGroup.Value = BOOL.True;
                    shape.Protection.LockHeight.Value = BOOL.True;
                    shape.Protection.LockMoveX.Value = BOOL.True;
                    shape.Protection.LockMoveY.Value = BOOL.True;
                    shape.Protection.LockRotate.Value = BOOL.True;
                    shape.Protection.LockSelect.Value = BOOL.True;
                    shape.Protection.LockTextEdit.Value = BOOL.True;
                    shape.Protection.LockThemeColors.Value = BOOL.True;
                    shape.Protection.LockThemeEffects.Value = BOOL.True;
                    shape.Protection.LockVtxEdit.Value = BOOL.True;
                    shape.Protection.LockWidth.Value = BOOL.True;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
