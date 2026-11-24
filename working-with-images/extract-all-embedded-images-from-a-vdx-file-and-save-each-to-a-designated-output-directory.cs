using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vdx> <output_directory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                int imageCounter = 0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify foreign (image) shapes
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Retrieve the raw image bytes
                            byte[] imageData = shape.ForeignData?.Value;
                            if (imageData == null || imageData.Length == 0)
                                continue;

                            // Build a unique file name for each extracted image
                            string imagePath = Path.Combine(outputDir, $"image_{imageCounter}.png");

                            // Write the image bytes directly to a file
                            File.WriteAllBytes(imagePath, imageData);
                            Console.WriteLine($"Extracted image saved to: {imagePath}");

                            imageCounter++;
                        }
                    }
                }

                if (imageCounter == 0)
                {
                    Console.WriteLine("No embedded images were found in the diagram.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}