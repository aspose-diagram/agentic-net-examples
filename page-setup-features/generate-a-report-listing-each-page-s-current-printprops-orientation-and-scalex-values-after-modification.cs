using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page to modify PrintProps and collect report data.
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Set orientation to Landscape for demonstration.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Set ScaleX to 0.75 (75% scaling) for demonstration.
                page.PageSheet.PrintProps.ScaleX.Value = 0.75;

                // Retrieve current values after modification.
                PrintPageOrientationValue orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                // Output report line for the current page.
                Console.WriteLine($"Page {pageIndex + 1}: Orientation = {orientation}, ScaleX = {scaleX}");

                pageIndex++;
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Dispose the diagram to free resources.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}