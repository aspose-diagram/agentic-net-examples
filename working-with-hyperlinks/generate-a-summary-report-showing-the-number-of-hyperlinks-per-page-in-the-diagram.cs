using System;
using System.IO;
using Aspose.Diagram;

class HyperlinkReport
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // Load the diagram using Aspose.Diagram (lifecycle: load)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Prepare a StringWriter to collect the report
                StringWriter reportWriter = new StringWriter();

                // Header for the report
                reportWriter.WriteLine("Hyperlink Count per Page");
                reportWriter.WriteLine("--------------------------");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the Hyperlink collection of the page's PageSheet
                    int hyperlinkCount = page.PageSheet.Hyperlinks.Count;

                    // Write the page name and hyperlink count to the report
                    reportWriter.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}): {hyperlinkCount} hyperlink(s)");
                }

                // Output the report to console
                Console.WriteLine(reportWriter.ToString());

                // Optionally, save the report to a text file (lifecycle: save)
                string reportPath = "HyperlinkReport.txt";
                File.WriteAllText(reportPath, reportWriter.ToString());
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
