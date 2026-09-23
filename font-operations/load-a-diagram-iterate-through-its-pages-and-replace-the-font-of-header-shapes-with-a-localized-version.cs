using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file path, output file path, localized font name
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <program> <inputPath> <outputPath> <localizedFontName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string localizedFont = args[2];

        // Verify that the requested font is installed on the system
        bool fontExists = false;
        var installedFonts = new InstalledFontCollection();
        foreach (var family in installedFonts.Families)
        {
            if (family.Name.Equals(localizedFont, StringComparison.OrdinalIgnoreCase))
            {
                fontExists = true;
                break;
            }
        }

        if (!fontExists)
        {
            Console.WriteLine($"Warning: Font \"{localizedFont}\" is not installed. Using default font.");
        }

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Set the default font for the diagram (fallback if a shape font is missing)
        FontConfigs.DefaultFontName = localizedFont;

        // Iterate through all pages
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Identify header shapes by a naming convention (e.g., name contains "Header")
                if (shape.NameU != null && shape.NameU.IndexOf("Header", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Ensure the shape has text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        // Update font for each character run in the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            ch.FontName.Value = localizedFont;
                        }
                    }
                }
            }
        }

        // Save the modified diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine("Diagram processing completed successfully.");
    }
}
