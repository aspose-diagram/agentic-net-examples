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

            // Load the existing Visio diagram
            Diagram sourceDiagram = new Diagram("source.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in sourceDiagram.Pages)
            {
                // Iterate backwards so that removal does not affect the index order
                for (int i = page.Shapes.Count - 1; i >= 0; i--)
                {
                    Shape shape = page.Shapes[i];

                    // Keep only shapes that belong to the "Export" layer
                    if (!IsShapeInLayer(shape, "Export"))
                    {
                        // Remove shapes that are not on the Export layer
                        page.Shapes.RemoveAt(i);
                    }
                }
            }

            // Save the filtered diagram to a new file (VDX format)
            sourceDiagram.Save("ExportOnly.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Placeholder method – replace with actual logic to determine layer membership
    static bool IsShapeInLayer(Shape shape, string layerName)
    {
        // Aspose.Diagram provides access to a shape's layer information via its ShapeSheet.
        // Implement the check according to the library's API, e.g., reading the "Layer" cell.
        // For compilation purposes, this stub returns false.
        return false;
    }
}
