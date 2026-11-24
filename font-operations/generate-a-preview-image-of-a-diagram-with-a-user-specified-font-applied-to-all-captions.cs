using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the diagram file path
        Console.Write("Enter diagram file path: ");
        string inputPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Prompt user for the output preview image path
        Console.Write("Enter output image path (e.g., preview.png): ");
        string outputPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            Console.Error.WriteLine("Output path cannot be empty.");
            return;
        }

        // Prompt user for the font name to apply
        Console.Write("Enter font name to apply to all captions: ");
        string fontName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fontName))
        {
            Console.Error.WriteLine("Font name cannot be empty.");
            return;
        }

        // Validate that the requested font is installed on the system
        var installedFonts = new InstalledFontCollection();
        bool fontFound = false;
        foreach (var family in installedFonts.Families)
        {
            if (string.Equals(family.Name, fontName, StringComparison.OrdinalIgnoreCase))
            {
                fontFound = true;
                break;
            }
        }

        if (!fontFound)
        {
            Console.Error.WriteLine($"Warning: Font '{fontName}' is not installed on this system. Aspose will use its fallback mechanism.");
        }

        // Set the default font for Aspose.Diagram rendering
        FontConfigs.DefaultFontName = fontName;

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages and shapes to apply the font to every character run
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply the font to each character in the shape
                        for (int i = 0; i < shape.Chars.Count; i++)
                        {
                            shape.Chars[i].FontName.Value = fontName;
                        }
                    }
                }

                // Configure image save options for a PNG preview of the first page
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.PageIndex = 0;   // Export the first page
                saveOptions.PageCount = 1;   // Only one page in the preview

                // Save the preview image
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Preview image successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}