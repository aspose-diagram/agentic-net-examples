using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Configure global default font
            // Ensure the font folder is known (recursive search)
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
            FontConfigs.DefaultFontName = "Arial";

            // Load the diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Store the default font name for comparison
            string defaultFont = FontConfigs.DefaultFontName;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check that the shape has at least one paragraph and one character
                    if (shape.Paras.Count > 0 && shape.Chars.Count > 0)
                    {
                        // Retrieve the font name of the first character (represents the first paragraph)
                        string fontName = shape.Chars[0].FontName.Value;

                        // Validate against the default font
                        if (!string.Equals(fontName, defaultFont, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"[Warning] Shape ID {shape.ID} on page '{page.Name}' uses font '{fontName}' instead of default '{defaultFont}'.");
                            // Uncomment the line below to treat this as a failure
                            // throw new Exception($"Font mismatch on shape ID {shape.ID}");
                        }
                    }
                    else
                    {
                        // Shape has no text; optionally report
                        Console.WriteLine($"[Info] Shape ID {shape.ID} on page '{page.Name}' contains no text.");
                    }
                }
            }

            Console.WriteLine("Font validation completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
