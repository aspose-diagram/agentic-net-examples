using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument or default suffix)
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_protected.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Check each custom property (Prop) of the shape
                    bool hasFixedAngleTrue = false;
                    for (int p = 0; p < shape.Props.Count; p++)
                    {
                        Prop prop = shape.Props[p];
                        if (prop.Name == "FixedAngle" &&
                            prop.Value != null &&
                            prop.Value.Val != null &&
                            string.Equals(prop.Value.Val, "true", StringComparison.OrdinalIgnoreCase))
                        {
                            hasFixedAngleTrue = true;
                            break;
                        }
                    }

                    // If the shape has FixedAngle set to true, apply protection
                    if (hasFixedAngleTrue)
                    {
                        // Lock rotation to prevent angle changes
                        shape.Protection.LockRotate.Value = BOOL.True;

                        // Optionally, you can lock other aspects here if needed
                        // e.g., shape.Protection.LockAspect.Value = BOOL.True;
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}