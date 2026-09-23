using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked for deletion
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape has a custom property named "Category" with value "Critical"
                    bool isCritical = false;
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "Category" && prop.Value.Val == "Critical")
                            {
                                isCritical = true;
                                break;
                            }
                        }
                    }

                    // Apply protection only to critical shapes
                    if (isCritical)
                    {
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        shape.Protection.LockMoveY.Value = BOOL.True;
                        shape.Protection.LockWidth.Value = BOOL.True;
                        shape.Protection.LockHeight.Value = BOOL.True;
                        shape.Protection.LockRotate.Value = BOOL.True;
                        shape.Protection.LockVtxEdit.Value = BOOL.True;
                        // Additional locks can be added here if needed
                    }
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
