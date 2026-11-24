using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, optional size threshold (in inches)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [sizeThreshold]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double sizeThreshold = 2.0; // default threshold in inches

        if (args.Length >= 3 && !double.TryParse(args[2], out sizeThreshold))
        {
            Console.Error.WriteLine("Invalid size threshold value.");
            return;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape dimensions (width and height in inches)
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // If either dimension exceeds the threshold, disable dynamic gluing
                        if (width > sizeThreshold || height > sizeThreshold)
                        {
                            // Set the glue type to NoAllowDynamicGlue to disable outgoing dynamic glue
                            shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}