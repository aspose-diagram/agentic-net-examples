using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram path, font to replace, replacement font, output diagram path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <oldFontName> <newFontName> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string oldFontName = args[1];
        string newFontName = args[2];
        string outputPath = args[3];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Optional: validate that the replacement font is installed on the system
        try
        {
            var installedFonts = new InstalledFontCollection();
            bool fontExists = installedFonts.Families.Any(f => string.Equals(f.Name, newFontName, StringComparison.OrdinalIgnoreCase));
            if (!fontExists)
            {
                Console.Error.WriteLine($"Warning: Replacement font \"{newFontName}\" is not installed on this system.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to validate installed fonts: {ex.Message}");
            // Continue even if validation fails
        }

        // Load the diagram, replace fonts, and save
        try
        {
            // Diagram must be instantiated inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through all character runs in the shape
                        for (int i = 0; i < shape.Chars.Count; i++)
                        {
                            // Access the FontName cell of the character run
                            // Replace the font name if it matches the old font (case‑insensitive)
                            if (string.Equals(shape.Chars[i].FontName.Value, oldFontName, StringComparison.OrdinalIgnoreCase))
                            {
                                shape.Chars[i].FontName.Value = newFontName;
                            }
                        }
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}