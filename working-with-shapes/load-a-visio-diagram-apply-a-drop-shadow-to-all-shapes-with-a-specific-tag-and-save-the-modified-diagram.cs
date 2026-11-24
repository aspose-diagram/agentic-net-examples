using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file path, tag value to match, output Visio file path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <tagValue> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string targetTagValue = args[1];
        string outputPath = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has custom properties (Props) collection
                        if (shape.Props == null)
                            continue;

                        // Look for a custom property named "Tag" with the desired value
                        bool tagMatches = false;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "Tag" && prop.Value != null && prop.Value.Val == targetTagValue)
                            {
                                tagMatches = true;
                                break;
                            }
                        }

                        if (!tagMatches)
                            continue;

                        // Apply a simple drop shadow to the shape
                        if (shape.Fill != null)
                        {
                            // Set shadow type to Simple
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                            // Set shadow color to black
                            shape.Fill.ShdwForegnd.Value = "#000000";
                            // Set shadow transparency to 30% (0.3)
                            shape.Fill.ShdwForegndTrans.Value = 0.3;
                            // Optional: set shadow offsets (default values can be used)
                            // shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                            // shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}