using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    bool hasFixedAngleTrue = false;

                    // Examine each custom property (Prop) of the shape.
                    foreach (Prop prop in shape.Props)
                    {
                        // Look for a property named "FixedAngle".
                        if (prop.Name == "FixedAngle")
                        {
                            // Ensure the value cell is present and compare its string value.
                            if (prop.Value != null && prop.Value.Val != null &&
                                string.Equals(prop.Value.Val, "true", StringComparison.OrdinalIgnoreCase))
                            {
                                hasFixedAngleTrue = true;
                                break;
                            }
                        }
                    }

                    // If the shape has FixedAngle = true, apply rotation lock protection.
                    if (hasFixedAngleTrue && shape.Protection != null)
                    {
                        // Lock the rotation of the shape.
                        shape.Protection.LockRotate.Value = BOOL.True;
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}