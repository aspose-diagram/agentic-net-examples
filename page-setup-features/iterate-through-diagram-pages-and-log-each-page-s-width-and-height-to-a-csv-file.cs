using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio diagram file (adjust as needed)
            string diagramPath = "input.vsdx";

            // Path to the CSV file that will contain page dimensions
            string csvPath = "pages_dimensions.csv";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Create or overwrite the CSV file and write the header
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("PageName,Width,Height");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page name and dimensions (in inches)
                    string pageName = page.NameU;
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    // Write a CSV line for the current page
                    writer.WriteLine($"{pageName},{width},{height}");
                }
            }

            Console.WriteLine($"Page dimensions have been logged to '{csvPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
