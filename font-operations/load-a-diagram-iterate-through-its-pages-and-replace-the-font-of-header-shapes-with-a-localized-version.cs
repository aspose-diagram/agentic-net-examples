using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Define the localized font name you want to apply
        const string localizedFontName = "Arial";

        // Verify that the font is installed on the system using Aspose.Drawing.Text
        InstalledFontCollection fontCollection = new InstalledFontCollection();
        bool fontExists = fontCollection.Families
            .Any(f => string.Equals(f.Name, localizedFontName, StringComparison.OrdinalIgnoreCase));

        if (!fontExists)
        {
            Console.WriteLine($"Warning: Font \"{localizedFontName}\" is not installed. The fallback font will be used.");
        }

        // Load the Visio diagram
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify header shapes by name (adjust the condition as needed)
                    if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("Header", StringComparison.OrdinalIgnoreCase))
                    {
                        // Ensure there is at least one Char object for formatting
                        if (shape.Chars.Count == 0)
                        {
                            Aspose.Diagram.Char newChar = new Aspose.Diagram.Char();
                            newChar.IX = 0; // start index
                            shape.Chars.Add(newChar);
                        }

                        // Apply the localized font to all character runs of the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            ch.FontName.Value = localizedFontName;
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }

        Console.WriteLine("Font replacement completed successfully.");
    }
}
