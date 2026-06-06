using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and safely modify its PageProps
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    // Retrieve current page dimensions
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    // Example modification: increase dimensions by 10%
                    page.PageSheet.PageProps.PageWidth.Value = width * 1.1;
                    page.PageSheet.PageProps.PageHeight.Value = height * 1.1;
                }
                catch (Exception ex)
                {
                    // Log any errors encountered while accessing PageProps
                    Console.WriteLine($"Error processing page '{page.Name}': {ex.Message}");
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
