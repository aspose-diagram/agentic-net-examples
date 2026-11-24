using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required.
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Iterate over all pages and configure print layout.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Explicitly type the page variable.
                    Page page = diagram.Pages[i];

                    // Set page orientation to Landscape.
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Set horizontal scaling factor to 0.9 (90%).
                    page.PageSheet.PrintProps.ScaleX.Value = 0.9;
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any Aspose or runtime errors to the error stream.
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}