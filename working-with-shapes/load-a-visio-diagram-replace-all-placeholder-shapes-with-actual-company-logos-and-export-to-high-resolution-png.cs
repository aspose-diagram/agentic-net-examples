using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, logo image file, output PNG file
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <logoImagePath> <outputPngPath>");
            return;
        }

        string inputVisioPath = args[0];
        string logoImagePath = args[1];
        string outputPngPath = args[2];

        // Guard: verify input Visio file exists
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        // Guard: verify logo image file exists
        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image file not found: {logoImagePath}");
            return;
        }

        // Load logo image bytes once
        byte[] logoBytes;
        try
        {
            logoBytes = File.ReadAllBytes(logoImagePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read logo image: {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block
        try
        {
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify placeholder shapes.
                        // This example treats any shape whose NameU contains "Placeholder"
                        // (case‑insensitive) as a placeholder to be replaced.
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf("Placeholder", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Replace the foreign image data with the logo bytes.
                            // Ensure the shape is of type Foreign (image) before assigning.
                            if (shape.Type == TypeValue.Foreign)
                            {
                                shape.ForeignData.Value = logoBytes;
                            }
                            else
                            {
                                // If the shape is not a foreign image, optionally convert it.
                                // For simplicity, we only replace existing foreign image shapes.
                                Console.WriteLine($"Shape '{shape.NameU}' is marked as placeholder but is not a foreign image. Skipping.");
                            }
                        }
                    }
                }

                // Prepare high‑resolution PNG export options.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Note: Aspose.Diagram.ImageSaveOptions does not expose a DPI property in this version.
                // The default export resolution is used.

                // Save the modified diagram as PNG.
                diagram.Save(outputPngPath, pngOptions);
                Console.WriteLine($"Diagram exported successfully to: {outputPngPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}