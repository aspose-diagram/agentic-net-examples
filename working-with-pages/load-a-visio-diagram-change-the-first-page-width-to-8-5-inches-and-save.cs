using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram has at least one page
            if (diagram.Pages.Count > 0)
            {
                // Access the first page (index 0)
                Page firstPage = diagram.Pages[0];

                // Set the page width to 8.5 inches (values are in inches)
                firstPage.PageSheet.PageProps.PageWidth.Value = 8.5;
            }
            else
            {
                Console.WriteLine("The diagram contains no pages to modify.");
            }

            // Save the modified diagram back to a Visio file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
