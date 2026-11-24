using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-vsdx-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool allImagesValid = true;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify embedded images (foreign shapes)
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Retrieve the raw image bytes from the shape's foreign data
                            byte[] imageBytes = shape.ForeignData?.Value;

                            // If there is no image data, continue to next shape
                            if (imageBytes == null || imageBytes.Length == 0)
                                continue;

                            // Load the image using Aspose.Drawing to inspect its DPI
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                // Fully qualify Aspose.Drawing.Image to avoid ambiguity
                                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                                {
                                    float horizontalDpi = img.HorizontalResolution;
                                    float verticalDpi = img.VerticalResolution;

                                    // Validate DPI against the 300 DPI minimum
                                    if (horizontalDpi < 300f || verticalDpi < 300f)
                                    {
                                        Console.WriteLine($"Image in shape ID {shape.ID} on page ID {page.ID} has DPI {horizontalDpi}x{verticalDpi}, which is below the required 300 DPI.");
                                        allImagesValid = false;
                                    }
                                }
                            }
                        }
                    }
                }

                // Report overall validation result
                if (allImagesValid)
                {
                    Console.WriteLine("All embedded images meet the minimum 300 DPI requirement.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}