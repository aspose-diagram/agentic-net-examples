using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has custom properties collection
                        if (shape.Props == null)
                            continue;

                        // Look for a custom property named "FixedAngle"
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "FixedAngle" &&
                                prop.Value != null &&
                                prop.Value.Val != null &&
                                prop.Value.Val.Equals("true", StringComparison.OrdinalIgnoreCase))
                            {
                                // Apply protection: lock rotation of the shape
                                shape.Protection.LockRotate.Value = BOOL.True;

                                // Optionally, you can lock other aspects if needed, e.g.:
                                // shape.Protection.LockMoveX.Value = BOOL.True;
                                // shape.Protection.LockMoveY.Value = BOOL.True;

                                // No need to continue checking other properties for this shape
                                break;
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