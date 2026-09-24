using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Prompt user for input Visio file path
        Console.Write("Enter path to the Visio file to modify: ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Prompt user for output file path
        Console.Write("Enter output file path (including .vsdx): ");
        string outputPath = Console.ReadLine();

        // Prompt for watermark text
        Console.Write("Enter watermark text: ");
        string watermarkText = Console.ReadLine();

        // Prompt for font name
        Console.Write("Enter font name (e.g., Calibri): ");
        string fontName = Console.ReadLine();

        // Prompt for font size in points and convert to inches
        Console.Write("Enter font size in points (e.g., 24): ");
        if (!double.TryParse(Console.ReadLine(), out double fontSizePoints))
        {
            Console.Error.WriteLine("Invalid font size.");
            return;
        }
        double fontSizeInches = fontSizePoints / 72.0; // 1 point = 1/72 inch

        // Prompt for font color as hex string
        Console.Write("Enter font color as hex string (e.g., #FF0000): ");
        string fontColor = Console.ReadLine();

        try
        {
            // Load the diagram inside a using block for proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a text shape that spans the whole page; AddText returns a Shape
                    Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                        watermarkText, fontName, fontColor, fontSizeInches);

                    // Clear any existing text runs and add the desired watermark text
                    watermarkShape.Text.Value.Clear();
                    watermarkShape.Text.Value.Add(new Txt(watermarkText));

                    // Apply character formatting (bold style, font name, size, color)
                    watermarkShape.Chars.Clear();
                    Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                    ch.IX = 0; // start index of the character run
                    ch.FontName.Value = fontName;
                    ch.Size.Value = fontSizeInches;
                    ch.Color.Value = fontColor;
                    ch.Style.Value = StyleValue.Bold; // apply bold style
                    watermarkShape.Chars.Add(ch);
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Watermark applied and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}