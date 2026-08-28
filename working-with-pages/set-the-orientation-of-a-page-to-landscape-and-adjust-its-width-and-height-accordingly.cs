using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output file path (second argument or default).
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Set the page orientation to Landscape.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Retrieve current page width and height (in inches).
                double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                double currentHeight = page.PageSheet.PageProps.PageHeight.Value;

                // If the page is currently taller than it is wide, swap dimensions.
                if (currentWidth < currentHeight)
                {
                    // Assign swapped values to achieve landscape dimensions.
                    page.PageSheet.PageProps.PageWidth.Value = currentHeight;
                    page.PageSheet.PageProps.PageHeight.Value = currentWidth;
                }
                // If width is already greater, keep existing dimensions (already landscape).
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any exception details to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}