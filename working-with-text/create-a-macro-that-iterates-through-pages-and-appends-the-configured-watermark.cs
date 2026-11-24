using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, optional watermark text
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [watermarkText]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string watermarkText = args.Length >= 3 ? args[2] : "Confidential";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and add the watermark text
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add watermark text to the page.
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize
                page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, "Calibri", "#a5a5a5", 0.25);
            }

            // Save the modified diagram in a macro-enabled format to preserve any existing VBA macros
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Watermark added and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}