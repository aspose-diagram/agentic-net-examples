using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
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
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Save the modified diagram back to a Visio format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("First page width set to 8.5 inches and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
