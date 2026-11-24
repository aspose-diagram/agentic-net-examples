using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for input diagram path
        Console.Write("Enter the path to the input Visio diagram: ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Prompt user for output diagram path
        Console.Write("Enter the desired output path (including file name): ");
        string outputPath = Console.ReadLine();

        // Prompt for watermark text
        Console.Write("Enter the watermark text: ");
        string watermarkText = Console.ReadLine();

        // Prompt for font name
        Console.Write("Enter the font name for the watermark (e.g., Calibri): ");
        string fontName = Console.ReadLine();

        // Prompt for font size in points and convert to inches (Aspose expects inches)
        Console.Write("Enter the font size in points (e.g., 24): ");
        if (!double.TryParse(Console.ReadLine(), out double fontSizePoints) || fontSizePoints <= 0)
        {
            Console.Error.WriteLine("Invalid font size.");
            return;
        }
        double fontSizeInches = fontSizePoints / 72.0;

        // Prompt for font color (hex string)
        Console.Write("Enter the font color as a hex string (e.g., #A5A5A5) or press Enter for default: ");
        string fontColor = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fontColor))
        {
            fontColor = "#A5A5A5"; // default light gray
        }

        // Prompt for style flags
        Console.Write("Enter style flags (comma separated: Bold, Italic, Underline) or press Enter for none: ");
        string styleInput = Console.ReadLine();
        StyleValue style = StyleValue.Undefined;
        if (!string.IsNullOrWhiteSpace(styleInput))
        {
            string[] parts = styleInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                string trimmed = part.Trim().ToLowerInvariant();
                if (trimmed == "bold")
                    style |= StyleValue.Bold;
                else if (trimmed == "italic")
                    style |= StyleValue.Italic;
                else if (trimmed == "underline")
                    style |= StyleValue.Underline;
            }
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page and add the watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve full page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Add watermark text covering the full page.
                    // Note: AddText overload does not support named parameters.
                    page.AddText(
                        0,                     // pinX
                        0,                     // pinY
                        pageWidth,             // width
                        pageHeight,            // height
                        watermarkText,         // text
                        fontName,              // font name
                        fontColor,             // font color (hex)
                        fontSizeInches);       // font size (in inches)

                    // The newly added shape is the last one in the collection
                    Shape watermarkShape = page.Shapes[page.Shapes.Count - 1];

                    // Apply style to all characters if a style was specified
                    if (style != StyleValue.Undefined)
                    {
                        for (int i = 0; i < watermarkShape.Chars.Count; i++)
                        {
                            // Use fully qualified Aspose.Diagram.Char to avoid ambiguity
                            Aspose.Diagram.Char ch = watermarkShape.Chars[i];
                            ch.Style.Value = style;
                        }
                    }
                }

                // Save the modified diagram using the correct SaveFileFormat enum member
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Watermark applied and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}