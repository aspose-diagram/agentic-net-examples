using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputPdfPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output PDF path
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            outputPath = Path.ChangeExtension(inputPath, ".pdf");
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a stylesheet with a solid light gray fill
            StyleSheet backgroundStyle = new StyleSheet();
            // Set solid fill pattern (1 = solid)
            backgroundStyle.Fill.FillPattern.Value = 1;
            // Light gray color hex
            backgroundStyle.Fill.FillForegnd.Value = "#D3D3D3";

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(backgroundStyle);

            // Retrieve the ID of the newly added stylesheet
            int styleId = diagram.StyleSheets[diagram.StyleSheets.Count - 1].ID;

            // Apply the stylesheet to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Apply the same style for line, fill, and character formatting
                page.ApplyStyle(styleId, styleId, styleId);
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format tracker
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Do not export hidden pages (optional, can be omitted)
            pdfOptions.ExportHiddenPage = false;

            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Successfully saved PDF to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}