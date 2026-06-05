using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        // Get input Visio file path from the user
        Console.Write("Enter the full path to the Visio file: ");
        string visioPath = Console.ReadLine();

        // Get desired font name from the user
        Console.Write("Enter the font name to apply to all captions: ");
        string userFont = Console.ReadLine();

        // Validate that the specified font is installed on the system
        var fontCollection = new InstalledFontCollection();
        bool fontExists = false;
        foreach (var family in fontCollection.Families)
        {
            if (family.Name.Equals(userFont, StringComparison.OrdinalIgnoreCase))
            {
                fontExists = true;
                break;
            }
        }

        if (!fontExists)
        {
            Console.WriteLine($"Font \"{userFont}\" not found. Falling back to Arial.");
            userFont = "Arial";
        }

        // Set the default font for the diagram (used for missing glyphs)
        FontConfigs.DefaultFontName = userFont;

        // Load the diagram
        Diagram diagram = new Diagram(visioPath);

        // Apply the chosen font to all shape captions (text)
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape actually contains text
                if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                {
                    // Update each character's font name
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        ch.FontName.Value = userFont;
                    }
                }
            }
        }

        // Prepare preview image save options (PNG format, first page only)
        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
        {
            PageIndex = 0 // render only the first page
        };

        // Determine output file path (same folder, name "preview.png")
        string outputDir = Path.GetDirectoryName(visioPath);
        string outputPath = Path.Combine(outputDir ?? string.Empty, "preview.png");

        // Save the preview image
        diagram.Save(outputPath, saveOptions);

        Console.WriteLine($"Preview image saved to: {outputPath}");
    }
}
