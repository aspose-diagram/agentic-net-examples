using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through each page and increase its width by 10 percent.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the current page width (in inches).
                    double currentWidth = page.PageSheet.PageProps.PageWidth.Value;

                    // Calculate the new width (increase by 10%).
                    double newWidth = currentWidth * 1.10;

                    // Apply the new width back to the page.
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                }

                // Overwrite the original file with the updated diagram.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
        }
    }
}