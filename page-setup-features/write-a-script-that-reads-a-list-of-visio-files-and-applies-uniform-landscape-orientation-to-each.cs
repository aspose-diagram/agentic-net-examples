using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that at least one file path is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath1> [<VisioFilePath2> ...]");
            return;
        }

        // Process each file path supplied via command line arguments.
        foreach (string filePath in args)
        {
            // Guard: ensure the file exists before attempting to load.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block for proper disposal.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate over all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Set the page orientation to Landscape.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }

                    // Save the modified diagram, overwriting the original file.
                    // Using VSDX format for consistency.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Successfully updated orientation to Landscape for: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}