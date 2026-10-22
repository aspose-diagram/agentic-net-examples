using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Identify background pages.
                    if (page.Background == BOOL.True)
                    {
                        // Set the print orientation of background pages to Portrait.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}