using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        const long MaxImageSizeBytes = 2L * 1024 * 1024; // 2 MB limit

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find embedded images.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify foreign (image) shapes.
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // The foreign data holds the raw image bytes.
                        // It may be null if the shape does not contain image data.
                        byte[] imageData = shape.ForeignData?.Value as byte[];
                        if (imageData != null)
                        {
                            long size = imageData.Length;
                            if (size > MaxImageSizeBytes)
                            {
                                Console.Error.WriteLine($"Image in shape ID {shape.ID} exceeds 2 MB limit (size: {size} bytes).");
                                return;
                            }
                        }
                    }
                }
            }

            // All images are within the size limit; proceed to export the diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}