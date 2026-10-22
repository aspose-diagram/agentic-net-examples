using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block as required by the API guidelines
            using (Diagram diagram = new Diagram())
            {
                // Retrieve the default page (a new Diagram contains one page by default)
                Page page = diagram.Pages[0];

                // Optionally set a custom name for the page
                page.Name = "CustomPage";

                // Set page dimensions (width and height) in inches
                // Use the PageProps collection to assign values directly to the .Value properties
                page.PageSheet.PageProps.PageWidth.Value = 8.5;   // width in inches
                page.PageSheet.PageProps.PageHeight.Value = 11.0; // height in inches

                // Save the diagram as VSDX using the correct SaveFileFormat enum
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}