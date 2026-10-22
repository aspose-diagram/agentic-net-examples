using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (required)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output Visio file path (required)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdm";
        // Watermark text (optional, default provided)
        string watermarkText = args.Length > 2 ? args[2] : "CONFIDENTIAL";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
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

            // Save the modified diagram as a macro-enabled Visio file
            diagram.Save(outputPath, SaveFileFormat.Vsdm);
            Console.WriteLine($"Watermark added and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}