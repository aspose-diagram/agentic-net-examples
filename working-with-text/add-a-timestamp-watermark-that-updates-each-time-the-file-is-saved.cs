using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // If output path not provided, create a new file name with suffix
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_watermarked.vsdx");

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and add a timestamp watermark
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Position the watermark at the top-left corner covering the whole page
                    double pinX = 0.0;
                    double pinY = 0.0;
                    double width = pageWidth;
                    double height = pageHeight;

                    // Create the watermark text with the current timestamp
                    string watermarkText = $"Generated on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                    // Font settings
                    string fontName = "Calibri";
                    string fontColor = "#A5A5A5"; // Light gray
                    double fontSize = 0.25; // Small size suitable for full-page watermark

                    // Add the watermark text shape to the page
                    page.AddText(pinX, pinY, width, height, watermarkText, fontName, fontColor, fontSize);
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with timestamp watermark to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}