using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page to determine its size.
            Page page = diagram.Pages[0];
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;   // inches
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value; // inches

            // Define known page sizes (in inches) with a tolerance.
            const double tolerance = 0.05; // 0.05 inch tolerance for size comparison.

            // A4 size: 8.27 x 11.69 inches
            bool isA4 = Math.Abs(pageWidth - 8.27) <= tolerance && Math.Abs(pageHeight - 11.69) <= tolerance;

            // Letter size: 8.5 x 11 inches
            bool isLetter = Math.Abs(pageWidth - 8.5) <= tolerance && Math.Abs(pageHeight - 11.0) <= tolerance;

            // Set header margin based on detected page size.
            if (isA4)
            {
                // 0.2 inches for A4.
                diagram.HeaderFooter.HeaderMargin.Value = 0.2;
                Console.WriteLine("Detected A4 page size. Header margin set to 0.2 inches.");
            }
            else if (isLetter)
            {
                // 0.3 inches for Letter.
                diagram.HeaderFooter.HeaderMargin.Value = 0.3;
                Console.WriteLine("Detected Letter page size. Header margin set to 0.3 inches.");
            }
            else
            {
                // Unknown size: default to 0.2 inches and inform the user.
                diagram.HeaderFooter.HeaderMargin.Value = 0.2;
                Console.WriteLine("Page size not recognized as A4 or Letter. Default header margin set to 0.2 inches.");
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}