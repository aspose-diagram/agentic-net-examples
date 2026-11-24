using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, logo image file, output folder
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <logoImagePath> <outputFolder>");
            return;
        }

        string inputVisioPath = args[0];
        string logoImagePath = args[1];
        string outputFolder = args[2];

        // Guard: input Visio file must exist
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        // Guard: logo image file must exist
        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image file not found: {logoImagePath}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
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
                // Iterate all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify placeholder shapes: foreign image shapes whose name contains "Placeholder"
                        if (shape.Type == TypeValue.Foreign && shape.NameU != null && shape.NameU.Contains("Placeholder"))
                        {
                            // Replace the foreign image data with the logo bytes
                            shape.ForeignData.Value = logoBytes;
                        }
                    }

                    // Export the current page to a high‑resolution PNG
                    string outputPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.png");
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export a single page
                        PageIndex = pageIndex,
                        PageCount = 1,
                        // High quality settings
                        JpegQuality = 100
                    };

                    try
                    {
                        diagram.Save(outputPath, pngOptions);
                        Console.WriteLine($"Exported page {pageIndex + 1} to {outputPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to export page {pageIndex + 1}: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}