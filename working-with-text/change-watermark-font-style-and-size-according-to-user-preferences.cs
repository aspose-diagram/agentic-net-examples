using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for input Visio file path
        Console.Write("Enter the path to the source Visio file: ");
        string inputPath = Console.ReadLine();
        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Prompt for output Visio file path
        Console.Write("Enter the desired output file path (e.g., output.vsdx): ");
        string outputPath = Console.ReadLine();

        // Prompt for watermark text
        Console.Write("Enter the watermark text: ");
        string watermarkText = Console.ReadLine();

        // Prompt for font name
        Console.Write("Enter the font name (e.g., Arial): ");
        string fontName = Console.ReadLine();

        // Prompt for font size in points
        Console.Write("Enter the font size in points (e.g., 36): ");
        string sizeInput = Console.ReadLine();
        if (!double.TryParse(sizeInput, out double fontSizePoints) || fontSizePoints <= 0)
        {
            Console.Error.WriteLine("Invalid font size.");
            return;
        }
        // Convert points to inches (Aspose.Diagram expects inches)
        double fontSizeInches = fontSizePoints / 72.0;

        // Prompt for style (Regular, Bold, Italic, BoldItalic)
        Console.Write("Enter the font style (Regular, Bold, Italic, BoldItalic): ");
        string styleInput = Console.ReadLine();
        // Determine the combined StyleValue based on user input
        StyleValue styleValue = StyleValue.Undefined;
        if (styleInput != null)
        {
            string styleUpper = styleInput.Trim().ToUpperInvariant();
            if (styleUpper.Contains("BOLD"))
                styleValue |= StyleValue.Bold;
            if (styleUpper.Contains("ITALIC"))
                styleValue |= StyleValue.Italic;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page to add the watermark
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate center position for the watermark
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Add a full‑page text shape as the watermark and capture the returned Shape object
                Shape watermarkShape = page.AddText(
                    centerX,               // pinX (center X)
                    centerY,               // pinY (center Y)
                    pageWidth,             // width (covers full page)
                    pageHeight,            // height (covers full page)
                    watermarkText,         // watermark text
                    fontName,              // user‑specified font name
                    "#808080",             // light gray color for watermark
                    fontSizeInches);       // font size in inches

                // Clear any existing character formatting
                watermarkShape.Chars.Clear();

                // Create a new character formatting entry
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = 0;                                 // character index
                ch.FontName.Value = fontName;              // set font name
                ch.Size.Value = fontSizeInches;            // set size (in inches)
                ch.Color.Value = "#808080";                // match the shape color
                ch.Style.Value = styleValue;               // apply bold/italic as needed

                // Add the character formatting to the shape
                watermarkShape.Chars.Add(ch);
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Watermark applied and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}