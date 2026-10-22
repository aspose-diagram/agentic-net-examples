using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input VSDX file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool allImagesValid = true;

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify embedded images: shapes of type Foreign.
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Ensure the shape contains foreign data (image bytes).
                            if (shape.ForeignData == null || shape.ForeignData.Value == null || shape.ForeignData.Value.Length == 0)
                            {
                                Console.WriteLine($"Warning: Shape ID {shape.ID} on page {page.ID} is marked as Foreign but contains no image data.");
                                allImagesValid = false;
                                continue;
                            }

                            // Load the image from the embedded byte array using Aspose.Drawing.Image.
                            using (MemoryStream ms = new MemoryStream(shape.ForeignData.Value))
                            {
                                // Fully qualify the Image class to avoid ambiguity with Aspose.Diagram.Image.
                                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                                {
                                    float hDpi = img.HorizontalResolution;
                                    float vDpi = img.VerticalResolution;

                                    // Validate DPI against the 300 DPI minimum.
                                    if (hDpi < 300f || vDpi < 300f)
                                    {
                                        Console.WriteLine($"Image validation failed for Shape ID {shape.ID} on page {page.ID}: DPI = {hDpi}x{vDpi} (required >= 300).");
                                        allImagesValid = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Image OK for Shape ID {shape.ID} on page {page.ID}: DPI = {hDpi}x{vDpi}.");
                                    }
                                }
                            }
                        }
                    }
                }

                // Final result output.
                if (allImagesValid)
                {
                    Console.WriteLine("All embedded images meet the minimum resolution of 300 DPI.");
                }
                else
                {
                    Console.WriteLine("One or more embedded images do not meet the minimum resolution of 300 DPI.");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors and report them.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}