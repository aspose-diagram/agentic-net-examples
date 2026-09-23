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
                    // Ensure the shape has custom properties collection
                    if (shape.Props != null)
                    {
                        // Look for a custom property named "FixedAngle"
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "FixedAngle" &&
                                string.Equals(prop.Value.Val, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                // Apply protection: lock rotation of the shape
                                shape.Protection.LockRotate.Value = BOOL.True;

                                // Additional locks can be added here if needed, e.g.:
                                // shape.Protection.LockMoveX.Value = BOOL.True;
                                // shape.Protection.LockMoveY.Value = BOOL.True;

                                break; // Property found, no need to check further props for this shape
                            }
                        }
                    }
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
