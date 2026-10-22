using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
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
                int pageIndex = 0;

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Check if the page has auto‑expand enabled.
                    if (page.PageSheet.PageProps.DrawingResizeType != null &&
                        page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically)
                    {
                        // Retrieve original dimensions (in inches).
                        double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Log original dimensions.
                        Console.WriteLine($"Page {pageIndex}: Auto‑expand enabled. Original size = {originalWidth}in x {originalHeight}in");

                        // Example resizing: increase dimensions by 10%.
                        double newWidth = originalWidth * 1.10;
                        double newHeight = originalHeight * 1.10;

                        // Apply new dimensions.
                        page.PageSheet.PageProps.PageWidth.Value = newWidth;
                        page.PageSheet.PageProps.PageHeight.Value = newHeight;

                        // Log new dimensions.
                        Console.WriteLine($"Page {pageIndex}: Resized to = {newWidth}in x {newHeight}in");
                    }

                    pageIndex++;
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}