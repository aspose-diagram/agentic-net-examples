using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Program <inputPath> <outputPath> [fontName] [lineSpacing]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fontName = args.Length > 2 ? args[2] : "Arial";
        double lineSpacing = 1.5;
        if (args.Length > 3 && double.TryParse(args[3], out double parsed))
            lineSpacing = parsed;

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Validate that the requested font is installed on the system.
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            bool fontInstalled = false;
            if (fontCollection.Families != null)
            {
                foreach (var f in fontCollection.Families)
                {
                    if (f != null && f.Name != null && f.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase))
                    {
                        fontInstalled = true;
                        break;
                    }
                }
            }

            if (!fontInstalled)
            {
                Console.Error.WriteLine($"Warning: Font '{fontName}' is not installed on this system. Fallback font will be used.");
            }

            // Set the default fallback font for rendering.
            FontConfigs.DefaultFontName = fontName;

            // Iterate over all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set the font name for every character run in the shape.
                    for (int ci = 0; ci < shape.Chars.Count; ci++)
                    {
                        shape.Chars[ci].FontName.Value = fontName;
                    }

                    // Set the line spacing for every paragraph in the shape.
                    for (int pi = 0; pi < shape.Paras.Count; pi++)
                    {
                        shape.Paras[pi].SpLine.Value = lineSpacing;
                    }
                }
            }

            // TODO: Save the modified diagram as DOCX.
            // Aspose.Diagram does not provide a direct DOCX export option.
            // Consider converting to an intermediate format (e.g., PDF) and then using Aspose.Words to create a DOCX,
            // or explore other libraries that support Visio‑to‑DOCX conversion.
            // Example placeholder:
            // diagram.Save(outputPath, /* appropriate save options for DOCX */);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}