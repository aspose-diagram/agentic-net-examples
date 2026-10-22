using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for the input Visio diagram path
        Console.Write("Enter path to input Visio diagram: ");
        string inputPath = Console.ReadLine();
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Prompt for the output diagram path
        Console.Write("Enter path for output diagram (e.g., output.vsdx): ");
        string outputPath = Console.ReadLine();

        // Prompt for watermark text
        Console.Write("Enter watermark text: ");
        string watermarkText = Console.ReadLine();

        // Prompt for font name
        Console.Write("Enter font name for watermark (e.g., Calibri): ");
        string fontName = Console.ReadLine();

        // Prompt for font size in points and convert to inches
        Console.Write("Enter font size in points (e.g., 24): ");
        string sizeInput = Console.ReadLine();
        if (!double.TryParse(sizeInput, out double fontSizePoints) || fontSizePoints <= 0)
        {
            Console.Error.WriteLine("Invalid font size.");
            return;
        }
        double fontSizeInches = fontSizePoints / 72.0;

        // Prompt for font color (hex string)
        Console.Write("Enter font color hex (e.g., #A5A5A5): ");
        string fontColor = Console.ReadLine();

        // Prompt for style keywords
        Console.Write("Enter style (None, Bold, Italic, Underline, combinations like BoldItalic): ");
        string styleInput = Console.ReadLine();

        // Parse style string into StyleValue flags
        StyleValue style = ParseStyle(styleInput);

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page and add the watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add watermark text covering the full page
                    // AddText returns a Shape object directly
                    Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                        watermarkText, fontName, fontColor, fontSizeInches);

                    // Apply the requested style to all characters in the watermark
                    for (int i = 0; i < watermarkShape.Chars.Count; i++)
                    {
                        watermarkShape.Chars[i].Style.Value = style;
                    }
                }

                // Determine the appropriate SaveFileFormat based on the output extension
                SaveFileFormat format = GetSaveFormat(outputPath);

                // Save the modified diagram using the two‑argument overload
                diagram.Save(outputPath, format);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Converts a user‑provided style string into StyleValue flags
    static StyleValue ParseStyle(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return StyleValue.Undefined;

        string s = input.Trim().ToLowerInvariant();
        StyleValue style = StyleValue.Undefined;

        if (s.Contains("bold"))
            style |= StyleValue.Bold;
        if (s.Contains("italic"))
            style |= StyleValue.Italic;
        if (s.Contains("underline"))
            style |= StyleValue.Underline;

        return style;
    }

    // Maps file extensions to the corresponding SaveFileFormat enum values
    static SaveFileFormat GetSaveFormat(string path)
    {
        string ext = Path.GetExtension(path).ToLowerInvariant();
        return ext switch
        {
            ".vsdx" => SaveFileFormat.Vsdx,
            ".vsd" => SaveFileFormat.Vsd,
            ".vdx" => SaveFileFormat.Vdx,
            ".pdf" => SaveFileFormat.Pdf,
            ".png" => SaveFileFormat.Png,
            ".jpg" => SaveFileFormat.Jpeg,
            ".jpeg" => SaveFileFormat.Jpeg,
            ".svg" => SaveFileFormat.Svg,
            ".html" => SaveFileFormat.Html,
            ".xps" => SaveFileFormat.Xps,
            ".gif" => SaveFileFormat.Gif,
            ".bmp" => SaveFileFormat.Bmp,
            ".emf" => SaveFileFormat.Emf,
            ".tiff" => SaveFileFormat.Tiff,
            ".swf" => SaveFileFormat.Swf,
            ".xaml" => SaveFileFormat.Xaml,
            ".vss" => SaveFileFormat.Vss,
            ".vssx" => SaveFileFormat.Vssx,
            ".vssm" => SaveFileFormat.Vssm,
            ".vst" => SaveFileFormat.Vst,
            ".vstx" => SaveFileFormat.Vstx,
            ".vstm" => SaveFileFormat.Vstm,
            ".csv" => SaveFileFormat.Csv,
            _ => SaveFileFormat.Vsdx // Default format
        };
    }
}