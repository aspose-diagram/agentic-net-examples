using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard against missing input file.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found – {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate over each page and increase its width by 10 %.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the current width (in inches).
                    double currentWidth = page.PageSheet.PageProps.PageWidth.Value;

                    // Calculate the new width (10 % larger).
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
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}