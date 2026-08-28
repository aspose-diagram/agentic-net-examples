using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Create or overwrite a text file to store the summary report
            using (StreamWriter writer = new StreamWriter("HyperlinkReport.txt"))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Count hyperlinks on the current page via its PageSheet
                    int hyperlinkCount = page.PageSheet.Hyperlinks.Count;

                    // Write the result to console
                    Console.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) has {hyperlinkCount} hyperlink(s).");

                    // Write the same information to the report file
                    writer.WriteLine($"Page \"{page.Name}\": {hyperlinkCount} hyperlink(s)");
                }
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
