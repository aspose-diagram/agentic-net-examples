using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file, output file, size threshold (double)
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <sizeThreshold>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string thresholdArg = args[2];

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse size threshold
        if (!double.TryParse(thresholdArg, out double sizeThreshold))
        {
            Console.Error.WriteLine($"Invalid size threshold: {thresholdArg}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape dimensions (in inches)
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // If either dimension exceeds the threshold, disable dynamic gluing
                        if (width > sizeThreshold || height > sizeThreshold)
                        {
                            // Ensure Misc (which holds GlueType) is available
                            if (shape.Misc != null)
                            {
                                shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Processing completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}