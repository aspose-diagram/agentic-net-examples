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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    double minLeft = double.MaxValue;
                    double maxRight = double.MinValue;

                    // Examine every shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape dimensions.
                        double pinX = shape.XForm.PinX.Value;          // Center X
                        double width = shape.XForm.Width.Value;        // Width

                        // Calculate left and right extents.
                        double left = pinX - (width / 2.0);
                        double right = pinX + (width / 2.0);

                        // Update bounding values.
                        if (left < minLeft) minLeft = left;
                        if (right > maxRight) maxRight = right;
                    }

                    // If at least one shape was found, adjust the page width.
                    if (minLeft != double.MaxValue && maxRight != double.MinValue)
                    {
                        double newWidth = maxRight - minLeft;

                        // Ensure the new width is positive.
                        if (newWidth > 0)
                        {
                            // Set the page width to tightly fit the content.
                            page.PageSheet.PageProps.PageWidth.Value = newWidth;
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}