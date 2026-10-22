using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, and tag name to search for.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <tagName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetTag = args[2];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine whether the shape has the specified tag (custom property).
                        bool hasTag = false;
                        if (shape.Props != null)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                if (prop.Name == targetTag)
                                {
                                    hasTag = true;
                                    break;
                                }
                            }
                        }

                        if (hasTag)
                        {
                            // Apply a simple drop shadow to the shape.
                            // Set shadow type.
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                            // Set shadow color (black).
                            shape.Fill.ShdwForegnd.Value = "#000000";
                            // Set shadow transparency (30% transparent).
                            shape.Fill.ShdwForegndTrans.Value = 0.3;
                            // Set shadow offsets.
                            shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                            shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                        }
                    }
                }

                // Save the modified diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}