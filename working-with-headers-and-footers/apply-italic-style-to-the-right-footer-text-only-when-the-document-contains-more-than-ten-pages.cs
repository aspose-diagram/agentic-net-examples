using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
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

            // Check the number of pages in the document.
            int pageCount = diagram.Pages.Count;

            // If the document has more than ten pages, apply italic style to the footer font.
            // Note: Aspose.Diagram applies font styling globally to all header/footer text.
            // There is no API to style only the right footer text separately.
            if (pageCount > 10)
            {
                // Enable italic styling.
                diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.True;
                Console.WriteLine("Italic style applied to header/footer font (document has more than 10 pages).");
            }
            else
            {
                // Ensure italic is not set when condition is not met.
                diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.False;
                Console.WriteLine("Document has 10 or fewer pages; italic style not applied.");
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}