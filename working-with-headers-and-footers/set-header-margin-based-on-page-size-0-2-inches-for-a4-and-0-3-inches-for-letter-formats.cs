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
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to evaluate size
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page to decide the page size
            Page firstPage = diagram.Pages[0];

            double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;   // inches
            double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value; // inches

            // Determine if the page size matches A4 or Letter (allow small tolerance)
            const double tolerance = 0.05; // inches
            double a4Width = 8.27, a4Height = 11.69;
            double letterWidth = 8.5, letterHeight = 11.0;

            double headerFooterMargin;

            bool isA4 = Math.Abs(pageWidth - a4Width) <= tolerance && Math.Abs(pageHeight - a4Height) <= tolerance;
            bool isLetter = Math.Abs(pageWidth - letterWidth) <= tolerance && Math.Abs(pageHeight - letterHeight) <= tolerance;

            if (isA4)
            {
                headerFooterMargin = 0.2; // inches for A4
            }
            else if (isLetter)
            {
                headerFooterMargin = 0.3; // inches for Letter
            }
            else
            {
                // Default margin if size is unrecognized
                headerFooterMargin = 0.25;
                Console.WriteLine("Page size does not match standard A4 or Letter. Applying default margin of 0.25 inches.");
            }

            // Set header and footer margins (both use the same value)
            diagram.HeaderFooter.HeaderMargin.Value = headerFooterMargin;
            diagram.HeaderFooter.FooterMargin.Value = headerFooterMargin;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}