using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVdxFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least two pages.
            if (diagram.Pages.Count < 2)
            {
                Console.Error.WriteLine("The diagram does not contain a second page.");
                return;
            }

            // Retrieve the second page (index 1).
            Page secondPage = diagram.Pages[1];

            // Read current page dimensions (in inches).
            double originalWidth = secondPage.PageSheet.PageProps.PageWidth.Value;
            double originalHeight = secondPage.PageSheet.PageProps.PageHeight.Value;
            Console.WriteLine($"Original page size - Width: {originalWidth} in, Height: {originalHeight} in");

            // Set the page height to 11 inches.
            secondPage.PageSheet.PageProps.PageHeight.Value = 11.0;
            Console.WriteLine("Page height set to 11 inches.");

            // Save the modified diagram to VDX format.
            diagram.Save(outputPath, SaveFileFormat.Vdx);
            Console.WriteLine($"Diagram successfully saved to VDX: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}