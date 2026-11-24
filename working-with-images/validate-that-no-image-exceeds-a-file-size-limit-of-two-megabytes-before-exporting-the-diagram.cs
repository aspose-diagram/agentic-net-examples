using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output file path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define the maximum allowed image size (2 MB).
            const long maxImageSizeBytes = 2 * 1024 * 1024;

            // Iterate through all pages and shapes to locate embedded images.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify foreign (image) shapes.
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        long imageSize = shape.ForeignData.Value.Length;

                        // Validate image size against the limit.
                        if (imageSize > maxImageSizeBytes)
                        {
                            Console.Error.WriteLine($"Embedded image in shape ID {shape.ID} exceeds 2 MB limit ({imageSize} bytes).");
                            return;
                        }
                    }
                }
            }

            // All images are within the size limit; proceed to export the diagram.
            diagram.Save(outputPath, SaveFileFormat.Pdf);
            Console.WriteLine($"Diagram exported successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}